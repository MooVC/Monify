namespace Monify.Snippets.Declarations.Classes;

using Microsoft.CodeAnalysis.CSharp;

internal static partial class SelfReferenced
{
    internal static class Annotations
    {
        public static readonly Content Generic = new("global::Monify.MonifyAttribute<SelfReferenced>", LanguageVersion.CSharp11);

        public static readonly Content NonGeneric = new("Monify(Type = typeof(SelfReferenced))", LanguageVersion.CSharp2);
    }
}