namespace Monify.Testing.Structs.Nested.InStruct;

public readonly ref partial struct OutterForString<T>
    where T : struct
{
    [Monify<string>]
    public readonly partial struct Inner;
}