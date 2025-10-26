namespace Monify.Console.Records.Nested.InRecordStruct;

public readonly partial record struct OutterForInt<T>
    where T : struct
{
    [Monify<int>]
    public sealed partial record Inner;
}