namespace Monify.Snippets.Declarations;

using Microsoft.CodeAnalysis.CSharp;

public static partial class Attributes
{
    public static class Declarations
    {
        public static readonly Generated Generic = new(
            AttributeGenerator.Generic,
            Extensions.All,
            "MonifyAttribute.Generic",
            Generator: typeof(AttributeGenerator));

        public static readonly Generated NonGeneric = new(
            AttributeGenerator.NonGeneric,
            Extensions.All,
            "MonifyAttribute.NonGeneric",
            Generator: typeof(AttributeGenerator));
    }
}