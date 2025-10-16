﻿namespace Monify.Syntax;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Monify.Model;
using Monify.Semantics;

/// <summary>
/// Provides extensions relating to <see cref="TypeDeclarationSyntax"/>.
/// </summary>
internal static partial class TypeDeclarationSyntaxExtensions
{
    /// <summary>
    /// Maps the required Semantics from the <paramref name="syntax"/>, using the <paramref name="compilation"/>
    /// and places it within an instance of <see cref="Subject"/>.
    ///
    /// The semantics will only be mapped if the <paramref name="syntax"/> is annotated with the Monify attribute and it,
    /// along with its parents, are marked as partial.
    /// </summary>
    /// <param name="syntax">
    /// The syntax for the class to be mapped.
    /// </param>
    /// <param name="compilation">
    /// Information relating to the compilation, used to obtain the semantic model for <paramref name="syntax"/>.
    /// </param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken" /> that can be used to cancel the operation.
    /// </param>
    /// <returns>
    /// When the <paramref name="syntax"/> is annotated with the Monify attribute and it, and its parents are marked as partial,
    /// the required semantics mapped from <paramref name="syntax"/> using <paramref name="compilation"/>, otherwise <see langword="null"/>.
    /// </returns>
    public static Subject? ToSubject(this TypeDeclarationSyntax? syntax, Compilation compilation, CancellationToken cancellationToken)
    {
        Stack<Nesting> nesting = new();

        if (syntax is null || !syntax.IsPartial())
        {
            return default;
        }

        SemanticModel model = compilation.GetSemanticModel(syntax.SyntaxTree);
        ISymbol? symbol = model.GetDeclaredSymbol(syntax, cancellationToken: cancellationToken);

        if (symbol is not INamedTypeSymbol type || !type.HasMonify(model, out ITypeSymbol value) || !type.IsSupported(nesting))
        {
            return default;
        }

        return type.ToSubject(compilation, ImmutableArray.ToImmutableArray(nesting), value);
    }
}