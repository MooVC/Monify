namespace Monify.Snippets.Declarations.Structs;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Snippets;

internal static class Unsupported
{
    public static readonly Snippets Declaration = new(
        Simple.Declaration.Body,
        new(
            $$"""
            namespace Monify.Testing.Structs
            {
                [{{BodyTag}}]
                public struct Unsupported
                {
                }
            }
            """,
            LanguageVersion.CSharp2),
        [],
        [],
        nameof(Unsupported));
}