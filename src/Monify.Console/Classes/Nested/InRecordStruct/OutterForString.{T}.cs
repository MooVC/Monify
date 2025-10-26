namespace Monify.Testing.Classes.Nested.InRecordStruct;

public readonly partial record struct OutterForString<T>
    where T : struct
{
    [Monify<string>]
    public sealed partial class Inner
    {
    }
}