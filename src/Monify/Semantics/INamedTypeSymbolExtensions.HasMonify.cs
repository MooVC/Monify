namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ISymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const int ExpectedGenericArgumentCountForMonifyAttribute = 1;
    private const int OffsetForEncapsulatedTypeOnMonifyAttribute = 1;

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
        value = symbol
            .GetAttributes()
            .Where(attribute => attribute.AttributeClass is not null
                && attribute.AttributeClass.IsGenericType
                && attribute.AttributeClass.TypeArguments.Length == ExpectedGenericArgumentCountForMonifyAttribute
                && attribute.AttributeClass.IsMonify())
            .Select(attribute => attribute.AttributeClass!.TypeArguments.ElementAt(OffsetForEncapsulatedTypeOnMonifyAttribute))
            .FirstOrDefault();

        if (value is not null)
        {
            return true;
        }

        value = symbol;

        return false;
    }
}