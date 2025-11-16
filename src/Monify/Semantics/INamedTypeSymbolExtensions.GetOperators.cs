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
    /// Gets the operator metadata that should be generated for the <paramref name="subject"/> based on the encapsulated value.
    /// </summary>
    /// <param name="subject">The subject to inspect.</param>
    /// <param name="value">The encapsulated value.</param>
    /// <returns>The operator metadata for the conversions and passthrough operators that should be generated.</returns>
    public static ImmutableArray<Operators> GetOperators(this INamedTypeSymbol subject, ITypeSymbol value)
    {
        ImmutableArray<Operators>.Builder builder = ImmutableArray.CreateBuilder<Operators>();

        builder.Add(new Operators
        {
            HasConversionFrom = subject.HasConversion(subject, value),
            HasConversionTo = subject.HasConversion(value, subject),
            HasEqualityOperator = subject.HasEqualityOperator(type: value),
            HasInequalityOperator = subject.HasInequalityOperator(type: value),
            Type = value.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
        });

        if (value is not INamedTypeSymbol named)
        {
            return builder.ToImmutable();
        }

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

            builder.Add(new Operators
            {
                HasConversionFrom = subject.HasConversion(subject, nested),
                HasConversionTo = subject.HasConversion(nested, subject),
                HasEqualityOperator = subject.HasEqualityOperator(type: nested),
                HasInequalityOperator = subject.HasInequalityOperator(type: nested),
                Type = nested.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
            });

            if (nested is not INamedTypeSymbol inner)
            {
                break;
            }

            named = inner;
        }

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