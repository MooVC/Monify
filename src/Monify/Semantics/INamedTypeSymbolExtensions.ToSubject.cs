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
    /// Maps the required Semantics from the <paramref name="subject"/> and places it within an instance of <see cref="Subject"/>.
    /// </summary>
    /// <param name="subject">
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
    public static Subject? ToSubject(this INamedTypeSymbol subject, Compilation compilation, ImmutableArray<Nesting> nesting, ITypeSymbol value)
    {
        string @namespace = subject.ContainingNamespace.IsGlobalNamespace
           ? string.Empty
           : subject.ContainingNamespace.ToDisplayString();

        string? declaration = subject.GetDeclaration();

        if (declaration is null
        || !subject.IsStateless(value, out bool hasFieldForEncapsulatedValue)
        || !subject.IsConstructable(value, out bool hasConstructorForEncapsulatedValue))
        {
            return default;
        }

        return new Subject
        {
            CanOverrideEquals = subject.CanOverrideEquals(),
            CanOverrideGetHashCode = subject.CanOverrideGetHashCode(),
            CanOverrideToString = subject.CanOverrideToString(),
            Declaration = declaration,
            HasConstructorForEncapsulatedValue = hasConstructorForEncapsulatedValue,
            HasConversionFrom = subject.HasConversion(subject),
            HasConversionTo = subject.HasConversion(value),
            HasEqualityOperatorForSelf = subject.HasEqualityOperator(),
            HasEqualityOperatorForValue = subject.HasEqualityOperator(type: value),
            HasEquatableForSelf = subject.HasEquatable(),
            HasEquatableForValue = subject.HasEquatable(type: value),
            HasFieldForEncapsulatedValue = hasFieldForEncapsulatedValue,
            HasInequalityOperatorForSelf = subject.HasInequalityOperator(),
            HasInequalityOperatorForValue = subject.HasInequalityOperator(type: value),
            IsEquatableToSelf = subject.IsEquatable(compilation),
            IsEquatableToValue = subject.IsEquatable(compilation, type: value),
            Name = subject.Name,
            Namespace = @namespace,
            Nesting = nesting,
            Qualification = subject.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
            Value = value.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat),
        };
    }
}
