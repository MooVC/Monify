namespace Monify.Snippets.Declarations.Records;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Declarations.Classes.SelfReferenced.Annotations;
using static Monify.Snippets.Snippets;

internal static partial class SelfReferenced
{
    public static readonly Snippets Declaration = new(
        [Generic, NonGeneric],
        new(
            $$"""
            namespace Monify.Testing.Records
            {
                [{{BodyTag}}]
                public sealed partial record SelfReferenced
                {
                }
            }
            """,
            LanguageVersion.CSharp9),
        [],
        [],
        nameof(SelfReferenced));
}