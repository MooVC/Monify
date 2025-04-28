namespace Monify.Semantics;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Monify.Model;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Maps the required Semantics from the <paramref name="class"/> and places it within an instance of <see cref="Subject"/>.
    /// </summary>
    /// <param name="class">
    /// The subject from which the semantics are identified.
    /// </param>
    /// <param name="compilation">
    /// The <see cref="Compilation"/> used to source the symbol for <see cref="IEquatable{T}"/>.
    /// </param>
    /// <param name="nesting">
    /// The declaration syntax for the parents of the <paramref name="syntax"/>.
    /// </param>
    /// <param name="value">
    /// The type of the value encapsulated by the <see cref="Subject"/>.
    /// </param>
    /// <returns>
    /// An instance of <see cref="Subject"/> containing the required semantics.
    /// </returns>
    /// <remarks>
    /// If the declaration associated with the type cannot be determined, the method will return <see langword="null" />.
    /// </remarks>
    public static Subject? ToSubject(this INamedTypeSymbol @class, Compilation compilation, ImmutableArray<Nesting> nesting, ITypeSymbol value)
    {
        string @namespace = @class.ContainingNamespace.IsGlobalNamespace
           ? string.Empty
           : @class.ContainingNamespace.ToDisplayString();

        string? declaration = @class.GetDeclaration();

        if (declaration is null
        || !@class.IsStateless(value, out bool hasFieldForEncapsulatedValue)
        || !@class.IsConstructable(value, out bool hasConstructorForEncapsulatedValue))
        {
            return default;
        }

        return new Subject
        {
            CanOverrideEquals = @class.CanOverrideEquals(),
            CanOverrideGetHashCode = @class.CanOverrideGetHashCode(),
            CanOverrideToString = @class.CanOverrideToString(),
            Declaration = declaration,
            HasConstructorForEncapsulatedValue = hasConstructorForEncapsulatedValue,
            HasEqualityOperatorForSelf = @class.HasEqualityOperator(),
            HasEqualityOperatorForValue = @class.HasEqualityOperator(type: value),
            HasEquatableForSelf = @class.HasEquatable(),
            HasEquatableForValue = @class.HasEquatable(type: value),
            HasFieldForEncapsulatedValue = hasFieldForEncapsulatedValue,
            HasInequalityOperatorForSelf = @class.HasInequalityOperator(),
            HasInequalityOperatorForValue = @class.HasInequalityOperator(type: value),
            IsEquatableToSelf = @class.IsEquatable(compilation),
            IsEquatableToValue = @class.IsEquatable(compilation, type: value),
            Name = @class.Name,
            Namespace = @namespace,
            Nesting = nesting,
            Qualification = @class.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
            Value = value.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
        };
    }
}