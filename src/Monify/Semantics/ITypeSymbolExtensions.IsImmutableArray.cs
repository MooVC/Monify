namespace Monify.Semantics;

using System;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="ITypeSymbol"/>.
/// </summary>
internal static partial class ITypeSymbolExtensions
{
    private const string ImmutableArrayMetadataName = "ImmutableArray`1";
    private const string ImmutableArrayNamespace = "System.Collections.Immutable";

    /// <summary>
    /// Determines whether or not the <paramref name="type"/> represents an <c>ImmutableArray&lt;T&gt;</c>.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns><see langword="true"/> if the <paramref name="type"/> represents an <c>ImmutableArray&lt;T&gt;</c>, otherwise <see langword="false"/>.</returns>
    public static bool IsImmutableArray(this ITypeSymbol type)
    {
        if (type is not INamedTypeSymbol namedType)
        {
            return false;
        }

        INamedTypeSymbol definition = namedType.OriginalDefinition;

        return string.Equals(definition.MetadataName, ImmutableArrayMetadataName, StringComparison.Ordinal)
            && string.Equals(definition.ContainingNamespace?.ToDisplayString(), ImmutableArrayNamespace, StringComparison.Ordinal);
    }
}