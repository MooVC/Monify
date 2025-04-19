namespace Monify.Snippets.Declarations;

public static class Attributes
{
    public static readonly Generated Monify = new(
        AttributeGenerator.Content,
        Extensions.None,
        $"{AttributeGenerator.Name}Attribute",
        typeof(AttributeGenerator));
}