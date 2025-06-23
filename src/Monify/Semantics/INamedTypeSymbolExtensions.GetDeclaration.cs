namespace Monify.Semantics;

using Microsoft.CodeAnalysis;

/// <summary>
/// Provides extensions relating to <see cref="INamedTypeSymbol"/>.
/// </summary>
internal static partial class INamedTypeSymbolExtensions
{
    /// <summary>
    /// Determines whether or not the <paramref name="subject"/> provided is supported by Monify.
    /// </summary>
    /// <param name="subject">
    /// The symbol for the type to be checked for Monify support.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the type is annotated and partial, otherwise <see langword="false"/>.
    /// </returns>
    public static string? GetDeclaration(this INamedTypeSymbol subject)
    {
        string prefix = subject.IdentifyPrefix();
        string type = subject.IdentifyType();

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

        string @sealed = symbol.TypeKind == TypeKind.Class
            ? "sealed"
            : string.Empty;

        return string
            .Join(" ", @readonly, @ref, @sealed)
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
