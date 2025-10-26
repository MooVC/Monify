namespace Monify.Testing.Structs.Nested.InRecord;

public sealed partial record OutterForArray<T>
    where T : struct
{
    [Monify<int[]>]
    public readonly partial struct Inner
    {
    }
}