namespace Monify.Snippets.Declarations;

public static class Internal
{
    public static readonly Generated HashCode = new(
        HashCodeGenerator.Content,
        Extensions.None,
        "Monify.Internal.HashCode",
        Generator: typeof(HashCodeGenerator));

    public static readonly Generated SequenceEqualityComparer = new(
        SequenceEqualityComparerGenerator.Content,
        Extensions.None,
        "Monify.Internal.SequenceEqualityComparer",
        Generator: typeof(SequenceEqualityComparerGenerator));
}