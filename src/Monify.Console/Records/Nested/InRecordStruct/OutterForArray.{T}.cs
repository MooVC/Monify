namespace Monify.Console.Records.Nested.InRecordStruct;

public readonly partial record struct OutterForArray<T>
    where T : struct
{
    [Monify<int[]>]
    public sealed partial record Inner;
}