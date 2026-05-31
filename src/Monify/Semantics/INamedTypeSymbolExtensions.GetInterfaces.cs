namespace Monify.Semantics;

using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    private const string CloneableTypeName = "System.ICloneable";
    private const string ComparableGenericTypeName = "System.IComparable`1";
    private const string ComparableTypeName = "System.IComparable";

    /// <summary>
    /// Gets the interfaces that can be forwarded from <paramref name="value"/>.
    /// </summary>
    /// <param name="value">
    /// The encapsulated value whose interfaces should be forwarded.
    /// </param>
    /// <param name="compilation">
    /// The compilation used to resolve well-known interface symbols.
    /// </param>
    /// <param name="equatables">
    /// The types for which equatable interfaces should be considered.
    /// </param>
    /// <param name="subject">
    /// The subject type being generated.
    /// </param>
    /// <returns>
    /// The interfaces that can be forwarded from <paramref name="value"/>.
    /// </returns>
    public static ImmutableArray<string> GetInterfaces(this INamedTypeSymbol value, Compilation compilation, ImmutableArray<ITypeSymbol> equatables, INamedTypeSymbol subject)
    {
        INamedTypeSymbol? cloneable = compilation.GetTypeByMetadataName(CloneableTypeName);
        INamedTypeSymbol? comparable = compilation.GetTypeByMetadataName(ComparableTypeName);
        INamedTypeSymbol? comparableGeneric = compilation.GetTypeByMetadataName(ComparableGenericTypeName);
        INamedTypeSymbol? equatable = compilation.GetTypeByMetadataName(EquatableTypeName);

        return value
            .AllInterfaces
            .Where(@interface => @interface.DeclaredAccessibility.CanForward(value, subject))
            .Where(@interface => @interface.CanForwardForSpecialType(value, comparable, comparableGeneric))
            .Where(@interface => @interface.CanForwardInterface(subject, cloneable))
            .Where(@interface => !subject.HasInterface(@interface))
            .Where(@interface => !@interface.IsEquatableForAny(equatable, equatables))
            .Select(@interface => @interface.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat))
            .Distinct()
            .ToImmutableArray();
    }

    private static bool CanForwardForSpecialType(
        this INamedTypeSymbol @interface,
        INamedTypeSymbol value,
        INamedTypeSymbol? comparable,
        INamedTypeSymbol? comparableGeneric)
    {
        return value.SpecialType == SpecialType.None
            || @interface.IsComparable(comparable)
            || @interface.IsComparableGeneric(comparableGeneric);
    }

    private static bool CanForwardInterface(this INamedTypeSymbol @interface, INamedTypeSymbol subject, INamedTypeSymbol? cloneable)
    {
        return !@interface.HasRequiredStaticMembers()
            && !@interface.IsCloneableForRecord(subject, cloneable);
    }

    private static bool HasRequiredStaticMembers(this INamedTypeSymbol @interface)
    {
        return @interface
            .AllInterfaces
            .Concat(new[] { @interface })
            .SelectMany(current => current.GetMembers())
            .Any(member => member.IsStatic && member.IsAbstract);
    }

    private static bool IsCloneableForRecord(this INamedTypeSymbol @interface, INamedTypeSymbol subject, INamedTypeSymbol? cloneable)
    {
        return subject.IsRecord
            && cloneable is not null
            && SymbolEqualityComparer.Default.Equals(@interface, cloneable);
    }

    private static bool HasInterface(this INamedTypeSymbol subject, INamedTypeSymbol @interface)
    {
        return subject.AllInterfaces.Any(candidate => SymbolEqualityComparer.Default.Equals(candidate, @interface));
    }

    private static bool IsComparable(this INamedTypeSymbol @interface, INamedTypeSymbol? comparable)
    {
        return comparable is not null
            && SymbolEqualityComparer.Default.Equals(@interface, comparable);
    }

    private static bool IsComparableGeneric(this INamedTypeSymbol @interface, INamedTypeSymbol? comparableGeneric)
    {
        return comparableGeneric is not null
            && SymbolEqualityComparer.Default.Equals(@interface.ConstructedFrom, comparableGeneric);
    }

    private static bool IsEquatableForAny(this INamedTypeSymbol @interface, INamedTypeSymbol? equatable, ImmutableArray<ITypeSymbol> values)
    {
        return values.Any(value => @interface.IsEquatableFor(equatable, value));
    }

    private static bool IsEquatableFor(this INamedTypeSymbol @interface, INamedTypeSymbol? equatable, ITypeSymbol value)
    {
        return equatable is not null
            && SymbolEqualityComparer.Default.Equals(@interface.ConstructedFrom, equatable)
            && @interface.TypeArguments.Length == ExpectedArgumentsForEquatable
            && SymbolEqualityComparer.Default.Equals(@interface.TypeArguments[EquatableTypeArgumentOffset], value);
    }
}