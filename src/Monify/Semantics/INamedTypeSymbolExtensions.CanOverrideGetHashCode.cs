namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="subject"/> can override <see cref="object.GetHashCode()"/>.
    /// </summary>
    /// <param name="subject">
    /// The <paramref name="subject"/> to be checked.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="subject"/> can override <see cref="object.GetHashCode()"/>, otherwise <see langword="false"/>.
    /// </returns>
    public static bool CanOverrideGetHashCode(this INamedTypeSymbol subject)
    {
        return subject.CanOverride(nameof(GetHashCode), SpecialType.System_Int32);
    }
}