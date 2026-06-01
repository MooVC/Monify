namespace Monify.Semantics;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Monify.Model;
using CSharpParseOptions = Microsoft.CodeAnalysis.CSharp.CSharpParseOptions;
using LanguageVersion = Microsoft.CodeAnalysis.CSharp.LanguageVersion;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Gets the metadata that should be generated for the <paramref name="subject"/> based on the encapsulated values.
    /// </summary>
    /// <param name="subject">The subject to inspect.</param>
    /// <param name="compilation">
    /// The <see cref="Compilation"/> used to source the symbol for <see cref="IEquatable{T}"/>.
    /// </param>
    /// <param name="model">
    /// The semantic model based for the current execution context.
    /// </param>
    /// <param name="value">The encapsulated value.</param>
    /// <returns>The metadata for the conversions and passthrough operators that should be generated.</returns>
    public static ImmutableArray<Encapsulated> GetEncapsulated(this INamedTypeSymbol subject, Compilation compilation, SemanticModel model, ITypeSymbol value)
    {
        ImmutableArray<Encapsulated>.Builder builder = ImmutableArray.CreateBuilder<Encapsulated>();
        IMethodSymbol[] constructors = subject.GetConstructors();
        ImmutableArray<ITypeSymbol> encapsulated = GetEncapsulatedValues(subject, value);
        ImmutableArray<ITypeSymbol> equatables = GetGeneratedEquatableTypes(subject, encapsulated);
        LanguageVersion languageVersion = GetLanguageVersion(model);

        if (encapsulated.IsDefaultOrEmpty)
        {
            return builder.ToImmutable();
        }

        builder.Add(Catalog(
            constructors,
            compilation,
            equatables,
            languageVersion,
            model,
            subject,
            encapsulated[0],
            includeForwardedMembers: true));

        foreach (ITypeSymbol passthrough in encapsulated.Skip(1))
        {
            builder.Add(Catalog(
                constructors,
                compilation,
                equatables,
                languageVersion,
                model,
                subject,
                passthrough,
                includeForwardedMembers: false));
        }

        return builder.ToImmutable();
    }

    private static Encapsulated Catalog(
        IMethodSymbol[] constructors,
        Compilation compilation,
        ImmutableArray<ITypeSymbol> equatables,
        LanguageVersion languageVersion,
        SemanticModel model,
        INamedTypeSymbol subject,
        ITypeSymbol value,
        bool includeForwardedMembers)
    {
        ImmutableArray<Conversion> conversions = ImmutableArray<Conversion>.Empty;
        ImmutableArray<string> interfaces = ImmutableArray<string>.Empty;
        ImmutableArray<PassthroughMethod> methods = ImmutableArray<PassthroughMethod>.Empty;
        ImmutableArray<PassthroughProperty> properties = ImmutableArray<PassthroughProperty>.Empty;
        ImmutableArray<BinaryOperator> binaryOperators = ImmutableArray<BinaryOperator>.Empty;
        ImmutableArray<UnaryOperator> unaryOperators = ImmutableArray<UnaryOperator>.Empty;

        if (value is INamedTypeSymbol encapsulated)
        {
            binaryOperators = encapsulated.GetBinaryOperators(compilation, subject);
            conversions = encapsulated.GetConversions(model, subject);
            unaryOperators = encapsulated.GetUnaryOperators(compilation, subject);

            if (includeForwardedMembers)
            {
                interfaces = encapsulated.GetInterfaces(compilation, equatables, subject);

                if (CanForwardMembers(encapsulated, interfaces))
                {
                    methods = encapsulated.GetPassthroughMethods(compilation, equatables, interfaces, languageVersion, subject);

                    if (CanForwardProperties(encapsulated))
                    {
                        properties = encapsulated.GetPassthroughProperties(interfaces, languageVersion, subject);
                    }
                }
            }
        }

        return new Encapsulated
        {
            BinaryOperators = binaryOperators,
            Conversions = conversions,
            HasConstructor = value.HasConstructorFor(constructors),
            HasConversionFrom = subject.HasConversion(subject, value),
            HasConversionTo = subject.HasConversion(value, subject),
            HasEqualityOperator = subject.HasEqualityOperator(type: value),
            HasEquatable = subject.HasEquatable(type: value),
            HasInequalityOperator = subject.HasInequalityOperator(type: value),
            Interfaces = interfaces,
            IsEquatable = subject.IsEquatable(compilation, type: value),
            IsSequence = value.IsSequence(),
            Methods = methods,
            Properties = properties,
            Type = value.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            UnaryOperators = unaryOperators,
        };
    }

    private static bool CanForwardMembers(INamedTypeSymbol encapsulated, ImmutableArray<string> interfaces)
    {
        return encapsulated.SpecialType == SpecialType.None
            || !interfaces.IsDefaultOrEmpty;
    }

    private static bool CanForwardProperties(INamedTypeSymbol encapsulated)
    {
        return encapsulated.SpecialType == SpecialType.None;
    }

    private static LanguageVersion GetLanguageVersion(SemanticModel model)
    {
        return model.SyntaxTree.Options is CSharpParseOptions options
            ? options.LanguageVersion
            : LanguageVersion.Default;
    }

    private static ImmutableArray<ITypeSymbol> GetEncapsulatedValues(INamedTypeSymbol subject, ITypeSymbol value)
    {
        ImmutableArray<ITypeSymbol>.Builder builder = ImmutableArray.CreateBuilder<ITypeSymbol>();

        HashSet<ITypeSymbol> visited = new(SymbolEqualityComparer.IncludeNullability)
        {
            subject,
        };

        if (!visited.Add(value))
        {
            return builder.ToImmutable();
        }

        builder.Add(value);

        if (value is not INamedTypeSymbol named)
        {
            return builder.ToImmutable();
        }

        while (TryGetEncapsulatedValue(named, out ITypeSymbol nested))
        {
            if (!visited.Add(nested))
            {
                break;
            }

            builder.Add(nested);

            if (nested is not INamedTypeSymbol inner)
            {
                break;
            }

            named = inner;
        }

        return builder.ToImmutable();
    }

    private static ImmutableArray<ITypeSymbol> GetGeneratedEquatableTypes(
        INamedTypeSymbol subject,
        ImmutableArray<ITypeSymbol> encapsulatedValues)
    {
        ImmutableArray<ITypeSymbol>.Builder builder = ImmutableArray.CreateBuilder<ITypeSymbol>();

        builder.Add(subject);
        builder.AddRange(encapsulatedValues);

        return builder.ToImmutable();
    }

    private static bool TryGetEncapsulatedValue(INamedTypeSymbol subject, out ITypeSymbol value)
    {
        foreach (AttributeData attribute in subject.GetAttributes())
        {
            if (attribute.AttributeClass is null || !attribute.AttributeClass.IsMonify())
            {
                continue;
            }

            if (attribute.AttributeClass.IsGenericType
             && attribute.AttributeClass.TypeArguments.Length == ExpectedGenericArgumentCountForMonifyAttribute)
            {
                value = attribute.AttributeClass.TypeArguments[OffsetForEncapsulatedTypeOnMonifyAttribute];

                return true;
            }

            if (TryGetEncapsulatedValueFromArguments(attribute, out value))
            {
                return true;
            }
        }

        value = subject;

        return false;
    }

    private static bool TryGetEncapsulatedValueFromArguments(AttributeData attribute, out ITypeSymbol value)
    {
        foreach (KeyValuePair<string, TypedConstant> argument in attribute.NamedArguments)
        {
            if (argument.Key == EncapsulatedValueTypeArgumentName
             && argument.Value.Kind == TypedConstantKind.Type
             && argument.Value.Value is ITypeSymbol symbol)
            {
                value = symbol;

                return true;
            }
        }

        value = attribute.AttributeClass!;

        return false;
    }
}