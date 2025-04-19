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
    /// The <see cref="Compilation"/> used to source related symbol for semantic extraction.
    /// </param>
    /// <param name="nesting">
    /// The declaration syntax for the parents of the <paramref name="syntax"/>.
    /// </param>
    /// <returns>
    /// An instance of <see cref="Subject"/> containing the required semantics.
    /// </returns>
    public static Subject ToSubject(this INamedTypeSymbol @class, Compilation compilation, ImmutableArray<Nesting> nesting)
    {
        string @namespace = @class.ContainingNamespace.IsGlobalNamespace
           ? string.Empty
           : @class.ContainingNamespace.ToDisplayString();

        return new Subject
        {
            Name = @class.Name,
            Namespace = @namespace,
            Nesting = nesting,
            Qualification = @class.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
        };
    }
}