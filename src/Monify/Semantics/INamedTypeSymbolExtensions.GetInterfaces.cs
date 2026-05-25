namespace Monify.Semantics;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Gets the interfaces that can be forwarded from <paramref name="value"/>.
    /// </summary>
    /// <param name="value">
    /// The encapsulated value whose interfaces should be forwarded.
    /// </param>
    /// <param name="compilation">
    /// The compilation used to resolve well-known interface symbols.
    /// </param>
    /// <param name="subject">
    /// The subject type being generated.
    /// </param>
    /// <returns>
    /// The interfaces that can be forwarded from <paramref name="value"/>.
    /// </returns>
    public static ImmutableArray<string> GetInterfaces(this INamedTypeSymbol value, Compilation compilation, INamedTypeSymbol subject)
    {
        INamedTypeSymbol? equatable = compilation.GetTypeByMetadataName(EquatableTypeName);

        return value
            .AllInterfaces
            .Where(@interface => @interface.DeclaredAccessibility.CanForward(value, subject))
            .Where(@interface => !@interface.IsEquatableFor(equatable, value))
            .Where(@interface => !@interface.IsEquatableFor(equatable, subject))
            .Select(@interface => @interface.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat))
            .Distinct()
            .ToImmutableArray();
    }

    private static bool IsEquatableFor(this INamedTypeSymbol @interface, INamedTypeSymbol? equatable, INamedTypeSymbol value)
    {
        return equatable is not null
            && SymbolEqualityComparer.Default.Equals(@interface.ConstructedFrom, equatable)
            && @interface.TypeArguments.Length == ExpectedArgumentsForEquatable
            && SymbolEqualityComparer.Default.Equals(@interface.TypeArguments[EquatableTypeArgumentOffset], value);
    }
}