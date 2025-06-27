namespace Monify.Snippets.Declarations;

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