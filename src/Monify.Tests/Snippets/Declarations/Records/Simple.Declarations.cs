namespace Monify.Snippets.Declarations.Records;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Snippets;

internal static partial class Simple
{
    public static class Declarations
    {
        public static readonly Content Main = new(
            $$"""
            namespace Monify.Testing.Records
            {
                [{{BodyTag}}]
                public partial record Record;
            }
            """,
            LanguageVersion.CSharp9);
    }
}