namespace Monify.Snippets.Declarations.Records;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Snippets;

internal static class Unsupported
{
    public static readonly Snippets Declaration = new(
        Simple.Declaration.Body,
        new(
            $$"""
            namespace Monify.Testing.Records
            {
                [{{BodyTag}}]
                public sealed record Unsupported
                {
                }
            }
            """,
            LanguageVersion.CSharp9),
        [],
        [],
        nameof(Unsupported));
}