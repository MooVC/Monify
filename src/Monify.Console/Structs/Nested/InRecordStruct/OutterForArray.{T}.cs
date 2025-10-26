namespace Monify.Console.Structs.Nested.InRecordStruct;

public readonly partial record struct OutterForArray<T>
    where T : struct
{
    [Monify<int[]>]
    public readonly partial struct Inner;
}