namespace Monify.Snippets.Declarations;

using Microsoft.CodeAnalysis.CSharp;

public static class Attributes
{
    public static readonly Generated Monify = new(
        AttributeGenerator.Content,
        Extensions.None,
        $"{AttributeGenerator.Name}Attribute",
        typeof(AttributeGenerator));

    public static class Declarations
    {
        public static readonly Content Generic = new("global::Monify.MonifyAttribute<int>", LanguageVersion.CSharp11);

        public static readonly Content NonGeneric = new("Monify(Type = typeof(int))", LanguageVersion.CSharp2);
    }
}