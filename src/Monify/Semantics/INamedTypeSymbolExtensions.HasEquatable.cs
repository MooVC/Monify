namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const int ExpectedParameters = 1;

    /// <summary>
    /// Determines whether or not the <paramref name="class"/> declares an implementaton for Equals.
    /// </summary>
    /// <param name="class">
    /// The <paramref name="class"/> to be checked.
    /// </param>
    /// <param name="type">
    /// The type for which the method is applied.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="class"/> implements Equals, otherwise <see langword="false"/>.
    /// </returns>
    public static bool HasEquatable(this INamedTypeSymbol @class, ITypeSymbol? type = default)
    {
        type ??= @class;

        return @class
            .GetMembers(nameof(Equals))
            .OfType<IMethodSymbol>()
            .Any(method => method.ReturnType.SpecialType == SpecialType.System_Boolean
                        && method.Parameters.Length == ExpectedParameters
                        && SymbolEqualityComparer.Default.Equals(method.Parameters[0].Type.OriginalDefinition, type));
    }
}