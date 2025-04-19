namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ISymbol"/>.
/// </summary>
internal static partial class ISymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="symbol"/> represents the Monify attribute.
    /// </summary>
    /// <param name="symbol">
    /// The symbol to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="symbol"/> is the Monify attribute, otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsMonify(this ISymbol? symbol)
    {
        return symbol.IsAttribute(AttributeGenerator.Name);
    }
}