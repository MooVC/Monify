namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ISymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const string EncapsulatedValueTypeArgumentName = "Type";
    private const int ExpectedGenericArgumentCountForMonifyAttribute = 1;
    private const int OffsetForEncapsulatedTypeOnMonifyAttribute = 0;

    /// <summary>
    /// Determines whether or not the <paramref name="symbol"/> provided is annotated with the Monify attribute.
    /// </summary>
    /// <param name="symbol">
    /// The symbol for the symbol to be checked for the presence of the Monify attribute.
    /// </param>
    /// <param name="value">
    /// The type of the value to be encapsulated by the <paramref name="symbol"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the Monify attribute is present on the <paramref name="symbol"/>, otherwise <see langword="false"/>.
    /// </returns>
    public static bool HasMonify(this INamedTypeSymbol symbol, out ITypeSymbol value)
    {
        AttributeData data = symbol
            .GetAttributes()
            .Where(attribute => attribute.AttributeClass is not null && attribute.AttributeClass.IsMonify())
            .Select(attribute => attribute)
            .FirstOrDefault();

        if (data is not null)
        {
            return GetEncapsulatedValueType(data.AttributeClass!, data, out value);
        }

        value = symbol;

        return false;
    }

    private static bool GetEncapsulatedValueType(INamedTypeSymbol attribute, AttributeData data, out ITypeSymbol value)
    {
        if (attribute.IsGenericType && attribute.TypeArguments.Length == ExpectedGenericArgumentCountForMonifyAttribute)
        {
            value = attribute.TypeArguments.ElementAt(OffsetForEncapsulatedTypeOnMonifyAttribute);

            return true;
        }

        foreach (KeyValuePair<string, TypedConstant> argument in data.NamedArguments)
        {
            if (argument.Key == EncapsulatedValueTypeArgumentName
                && argument.Value.Kind == TypedConstantKind.Type
                && argument.Value.Value is ITypeSymbol symbol)
            {
                value = symbol;

                return true;
            }
        }

        value = attribute;

        return false;
    }
}