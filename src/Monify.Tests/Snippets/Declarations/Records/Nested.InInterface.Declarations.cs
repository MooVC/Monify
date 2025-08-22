namespace Monify.Snippets.Declarations.Records;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Snippets;

internal static partial class Nested
{
    public static partial class InInterface
    {
        public static class Declarations
        {
            public static readonly Content Main = new(
                $$"""
                namespace Monify.Testing.Records
                {
                    public partial interface IOutter<T>
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