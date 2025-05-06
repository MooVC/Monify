namespace Monify.Snippets.Declarations;

using Microsoft.CodeAnalysis.CSharp;

internal static partial class Record
{
    public static class Declarations
    {
        public static readonly Content Main = new(
            """
            namespace Monify.Records.Testing
            {
                [Monify<int>]
                public partial record Record;
            }
            """,
            LanguageVersion.CSharp9);
    }
}