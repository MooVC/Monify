namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ISymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="symbol"/> provided is annotated with the Monify attribute.
    /// </summary>
    /// <param name="symbol">
    /// The symbol for the symbol to be checked for the presence of the Monify attribute.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the Monify attribute is present on the <paramref name="symbol"/>, otherwise <see langword="false"/>.
    /// </returns>
    public static bool HasMonify(this INamedTypeSymbol symbol)
    {
        return symbol.HasAttribute(attribute => attribute.AttributeClass.IsMonify());
    }
}