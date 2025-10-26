namespace Monify.Testing.Records.Nested.InStruct;

public readonly ref partial struct OutterForString<T>
    where T : struct
{
    [Monify<string>]
    public sealed partial record Inner;
}