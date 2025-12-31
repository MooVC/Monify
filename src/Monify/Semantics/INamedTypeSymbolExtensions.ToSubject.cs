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
    /// <param name="model">
    /// The semantic model based for the current execution context.
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
    public static Subject? ToSubject(
        this INamedTypeSymbol subject,
        Compilation compilation,
        SemanticModel model,
        ImmutableArray<Nesting> nesting,
        ITypeSymbol value)
    {
        string @namespace = subject.ContainingNamespace.IsGlobalNamespace
           ? string.Empty
           : subject.ContainingNamespace.ToDisplayString();

        string? declaration = subject.GetDeclaration();

        if (declaration is null
         || subject.Equals(value, SymbolEqualityComparer.IncludeNullability)
         || !subject.IsStateless(value, out bool hasFieldForEncapsulatedValue))
        {
            return default;
        }

        return new Subject
        {
            CanOverrideEquals = subject.CanOverrideEquals(),
            CanOverrideGetHashCode = subject.CanOverrideGetHashCode(),
            CanOverrideToString = subject.CanOverrideToString(),
            Declaration = declaration,
            Encapsulated = subject.GetEncapsulated(compilation, model, value),
            HasEqualityOperator = subject.HasEqualityOperator(),
            HasEquatable = subject.HasEquatable(),
            HasField = hasFieldForEncapsulatedValue,
            HasInequalityOperator = subject.HasInequalityOperator(),
            IsEquatable = subject.IsEquatable(compilation),
            Name = subject.Name,
            Namespace = @namespace,
            Nesting = nesting,
            Qualification = subject.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
        };
    }
}