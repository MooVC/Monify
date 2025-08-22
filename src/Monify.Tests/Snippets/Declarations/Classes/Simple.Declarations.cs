namespace Monify.Snippets.Declarations.Classes;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Snippets;

internal static partial class Simple
{
    public static class Declarations
    {
        public static readonly Content Main = new(
            $$"""
            namespace Monify.Testing.Classes
            {
                [{{BodyTag}}]
                public partial class Simple
                {
                }
            }
            """,
            LanguageVersion.CSharp2);
    }
}