namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ISymbol"/>.
/// </summary>
internal static partial class ISymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="subject"/> represents the Monify attribute.
    /// </summary>
    /// <param name="subject">
    /// The symbol to check.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="subject"/> is the Monify attribute, otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsMonify(this ISymbol? subject)
    {
        return subject.IsAttribute(AttributeGenerator.Name);
    }
}
