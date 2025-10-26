namespace Monify.Testing.Structs.Nested.InStruct;

public readonly ref partial struct OutterForInt<T>
    where T : struct
{
    [Monify<int>]
    public readonly partial struct Inner
    {
    }
}