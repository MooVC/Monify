namespace Monify.Snippets.Declarations.Structs;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Declarations.Classes.SelfReferenced.Annotations;
using static Monify.Snippets.Snippets;

internal static partial class SelfReferenced
{
    public static readonly Snippets Declaration = new(
        [Generic, NonGeneric],
        new(
            $$"""
            namespace Monify.Testing.Structs
            {
                [{{BodyTag}}]
                public partial struct SelfReferenced
                {
                }
            }
            """,
            LanguageVersion.CSharp2),
        [],
        [],
        nameof(SelfReferenced));
}