namespace Monify.Semantics;

using Microsoft.CodeAnalysis;
using Monify.Model;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="value"/> is the same as the <paramref name="subject"/>, indicating a circular encapsulation.
    /// </summary>
    /// <param name="subject">
    /// The subject from which the semantics are identified.
    /// </param>
    /// <param name="value">
    /// The type of the value encapsulated by the <see cref="Subject"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="subject"/> is the same as the <paramref name="value"/>, otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsSelf(this INamedTypeSymbol subject, ITypeSymbol value)
    {
        return SymbolEqualityComparer.Default.Equals(subject, value);
    }
}
