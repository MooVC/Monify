namespace Monify.Snippets.Declarations.Classes;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Snippets;

internal static class Unsupported
{
    public static readonly Snippets Declaration = new(
        Simple.Declaration.Body,
        new(
            $$"""
            namespace Monify.Testing.Classes
            {
                [{{BodyTag}}]
                public sealed class Unsupported
                {
                }
            }
            """,
            LanguageVersion.CSharp2),
        [],
        [],
        nameof(Unsupported));
}