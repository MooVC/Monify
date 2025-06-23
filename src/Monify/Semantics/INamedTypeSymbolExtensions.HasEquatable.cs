namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="subject"/> declares an implementaton for Equals.
    /// </summary>
    /// <param name="subject">
    /// The <paramref name="subject"/> to be checked.
    /// </param>
    /// <param name="type">
    /// The type for which the method is applied.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="subject"/> implements Equals, otherwise <see langword="false"/>.
    /// </returns>
    public static bool HasEquatable(this INamedTypeSymbol subject, ITypeSymbol? type = default)
    {
        type ??= subject;

        return subject
            .GetMembers(nameof(Equals))
            .OfType<IMethodSymbol>()
            .Any(method => method.ReturnType.SpecialType == SpecialType.System_Boolean
                        && method.Parameters.Length == ExpectedParameterCountForEqualsMethod
                        && SymbolEqualityComparer.Default.Equals(method.Parameters[OffsetForObjectParameterOnEqualsMethod].Type.OriginalDefinition, type));
    }
}
