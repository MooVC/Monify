namespace Monify.Semantics;

using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Monify.Model;
using Monify.Syntax;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="symbol"/> provided is supported by Monify.
    /// </summary>
    /// <param name="symbol">
    /// The symbol for the type to be checked for Monify support.
    /// </param>
    /// <param name="nesting">
    /// The declaration syntax for the parents of the <paramref name="syntax"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the type is annotated and partial, otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsSupported(this INamedTypeSymbol symbol, Stack<Nesting> nesting)
    {
        INamedTypeSymbol? current = symbol;

        do
        {
            current = current.ContainingType;

            if (current is null)
            {
                return true;
            }

            bool isPartial = current.DeclaringSyntaxReferences
                .OfType<TypeDeclarationSyntax>()
                .All(type => type.IsPartial());

            if (!isPartial)
            {
                return false;
            }

            string? declaration = symbol.GetDeclaration();

            if (declaration is null)
            {
                return false;
            }

            var parent = new Nesting
            {
                Declaration = declaration,
                Name = current.Name,
                Qualification = current.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat),
            };

            nesting.Push(parent);
        }
        while (true);
    }
}