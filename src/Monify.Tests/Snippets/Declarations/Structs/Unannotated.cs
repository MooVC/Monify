namespace Monify.Snippets.Declarations.Structs;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Declarations.Attributes.Annotations;

internal static class Unannotated
{
    public static readonly Snippets Declaration = new(
        [Generic, NonGeneric],
        new(
            """
            namespace Monify.Testing.Structs
            {
                public partial struct Unannotated
                {
                }
            }
            """,
            LanguageVersion.CSharp2),
        [],
        [],
        nameof(Unannotated));
}