namespace Monify.Testing.Structs.Nested.InRecordStruct;

public readonly partial record struct OutterForString<T>
    where T : struct
{
    [Monify<string>]
    public readonly partial struct Inner;
}