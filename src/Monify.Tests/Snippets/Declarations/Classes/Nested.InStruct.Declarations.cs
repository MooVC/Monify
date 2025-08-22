namespace Monify.Snippets.Declarations.Classes;

using Microsoft.CodeAnalysis.CSharp;
using static Monify.Snippets.Snippets;

internal static partial class Nested
{
    public static partial class InStruct
    {
        public static class Declarations
        {
            public static readonly Content Main = new(
                $$"""
                namespace Monify.Testing.Classes
                {
                    public readonly ref partial struct Outter<T>
                        where T : struct
                    {
                        [{{BodyTag}}]
                        public sealed partial class Inner
                        {
                        }
                    }
                }
                """,
                LanguageVersion.CSharp7_2);
        }
    }
}