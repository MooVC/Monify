namespace Monify.Testing.Structs.Nested.InRecordStruct;

public readonly partial record struct OutterForInt<T>
    where T : struct
{
    [Monify<int>]
    public readonly partial struct Inner
    {
    }
}