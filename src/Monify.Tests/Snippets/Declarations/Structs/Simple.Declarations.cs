namespace Monify.Snippets.Declarations.Structs;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Snippets;

internal static partial class Simple
{
    public static class Declarations
    {
        public static readonly Content Main = new(
            $$"""
            namespace Monify.Testing.Structs
            {
                [{{BodyTag}}]
                public partial struct Simple
                {
                }
            }
            """,
            LanguageVersion.CSharp2);
    }
}