namespace Monify.Semantics;

using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Monify.Model;

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
    /// <param name="value">The encapsulated value.</param>
    /// <returns>The metadata for the conversions and passthrough operators that should be generated.</returns>
    public static ImmutableArray<Encapsulated> GetEncapsulated(this INamedTypeSymbol subject, Compilation compilation, ITypeSymbol value)
    {
        ImmutableArray<Encapsulated>.Builder builder = ImmutableArray.CreateBuilder<Encapsulated>();
        IMethodSymbol[] constructors = subject.GetConstructors();

        builder.Add(Catalog(constructors, compilation, subject, value));

        if (value is INamedTypeSymbol named)
        {
            GetPassthroughEncapsulations(builder, constructors, compilation, subject, named);
        }

        return builder.ToImmutable();
    }

    private static Encapsulated Catalog(IMethodSymbol[] constructors, Compilation compilation, INamedTypeSymbol subject, ITypeSymbol value)
    {
        ImmutableArray<Conversion> conversions = ImmutableArray<Conversion>.Empty;

        if (value is INamedTypeSymbol namedEncapsulated)
        {
            conversions = namedEncapsulated.GetConversions(subject);
        }

        return new Encapsulated
        {
            Conversions = conversions,
            HasConstructor = value.HasConstructorFor(constructors),
            HasConversionFrom = subject.HasConversion(subject, value),
            HasConversionTo = subject.HasConversion(value, subject),
            HasEqualityOperator = subject.HasEqualityOperator(type: value),
            HasEquatable = subject.HasEquatable(type: value),
            HasInequalityOperator = subject.HasInequalityOperator(type: value),
            IsEquatable = subject.IsEquatable(compilation, type: value),
            IsSequence = value.IsSequence(),
            Type = value.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
        };
    }

    private static void GetPassthroughEncapsulations(
        ImmutableArray<Encapsulated>.Builder builder,
        IMethodSymbol[] constructors,
        Compilation compilation,
        INamedTypeSymbol subject,
        INamedTypeSymbol named)
    {
        HashSet<ITypeSymbol> visited = new(SymbolEqualityComparer.IncludeNullability)
        {
            subject,
            named,
        };

        while (TryGetEncapsulatedValue(named, out ITypeSymbol nested))
        {
            if (!visited.Add(nested))
            {
                break;
            }

            builder.Add(Catalog(constructors, compilation, subject, nested));

            if (nested is not INamedTypeSymbol inner)
            {
                break;
            }

            named = inner;
        }
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