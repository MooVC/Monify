namespace Monify.Testing.Records.Nested.InRecordStruct;

public readonly partial record struct OutterForString<T>
    where T : struct
{
    [Monify<string>]
    public sealed partial record Inner
    {
    }
}