namespace Monify.Snippets.Declarations.Records;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Snippets;

internal static partial class Nested
{
    public static partial class InRecord
    {
        public static class Declarations
        {
            public static readonly Content Main = new(
                $$"""
                namespace Monify.Testing.Records
                {
                    public sealed partial record Outter<T>
                        where T : struct
                    {
                        [{{BodyTag}}]
                        public sealed partial record Inner
                        {
                        }
                    }
                }
                """,
                LanguageVersion.CSharp9);
        }
    }
}