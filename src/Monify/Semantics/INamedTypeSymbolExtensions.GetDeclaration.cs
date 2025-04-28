namespace Monify.Semantics;

using System.Collections.Generic;
using System.Xml.Linq;
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
    public static string? GetDeclaration(this INamedTypeSymbol symbol)
    {
        string prefix = symbol.IdentifyPrefix();
        string type = symbol.IdentifyType();

        return string
            .Join(" ", prefix, "partial", type)
            .TrimStart();
    }

    private static string IdentifyPrefix(this INamedTypeSymbol symbol)
    {
        string @ref = symbol.IsRefLikeType
            ? "ref"
            : string.Empty;

        string @readonly = symbol.IsReadOnly
            ? "readonly"
            : string.Empty;

        return string
            .Join(" ", @readonly, @ref)
            .Trim();
    }

    private static string IdentifyType(this INamedTypeSymbol symbol)
    {
        return symbol.TypeKind switch
        {
            TypeKind.Class => symbol.IsRecord
                ? "record"
                : "class",
            TypeKind.Struct => symbol.IsRecord
                ? "record struct"
                : "struct",
            TypeKind.Interface => "interface",
            _ => string.Empty,
        };
    }
}