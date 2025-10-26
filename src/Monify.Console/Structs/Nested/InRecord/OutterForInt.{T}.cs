namespace Monify.Testing.Structs.Nested.InRecord;

public sealed partial record OutterForInt<T>
    where T : struct
{
    [Monify<int>]
    public readonly partial struct Inner
    {
    }
}