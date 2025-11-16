namespace Monify.Semantics;

using Microsoft.CodeAnalysis;
using Monify.Strategies;

/// <summary>
/// Provides extensions relating to <see cref="ITypeSymbol"/>.
/// </summary>
internal static partial class ITypeSymbolExtensions
{
    private const int OffsetForConstructorParameterWhenDefined = 0;
    private const int ExpectedParametersForConstructorWhenDefined = 1;

    /// <summary>
    /// Determines whether or not <paramref name="constructors"/> supports <paramref name="value"/>.
    /// </summary>
    /// <param name="value">
    /// The type associated with the encapsulated value.
    /// </param>
    /// <param name="constructors">
    /// The constructors belonging to the subject for which constructability is to be determined.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="constructors"/> contains a constructor for <paramref name="value"/>, otherwise <see langword="false"/>.
    /// </returns>
    public static bool HasConstructorFor(this ITypeSymbol value, IMethodSymbol[] constructors)
    {
        return constructors.Any(member =>
            member.Parameters.Length == ExpectedParametersForConstructorWhenDefined
         && SymbolEqualityComparer.IncludeNullability.Equals(member.Parameters[OffsetForConstructorParameterWhenDefined].Type, value));
    }
}