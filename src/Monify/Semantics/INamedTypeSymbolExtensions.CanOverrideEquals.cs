namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const int ExpectedParameterCountForEqualsMethod = 1;
    private const int OffsetForObjectParameterOnEqualsMethod = 0;

    /// <summary>
    /// Determines whether or not the <paramref name="subject"/> can override <see cref="object.Equals(object)"/>.
    /// </summary>
    /// <param name="subject">
    /// The <paramref name="subject"/> to be checked.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="subject"/> can override <see cref="object.Equals(object)"/>, otherwise <see langword="false"/>.
    /// </returns>
    public static bool CanOverrideEquals(this INamedTypeSymbol subject)
    {
        return subject.CanOverride(
            nameof(Equals),
            SpecialType.System_Boolean,
            predicate: method => method.Parameters.Length == ExpectedParameterCountForEqualsMethod
                    && method.Parameters[OffsetForObjectParameterOnEqualsMethod].Type.SpecialType == SpecialType.System_Object);
    }
}