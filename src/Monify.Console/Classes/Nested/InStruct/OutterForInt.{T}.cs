namespace Monify.Testing.Classes.Nested.InStruct;

public readonly ref partial struct OutterForInt<T>
    where T : struct
{
    [Monify<int>]
    public sealed partial class Inner
    {
    }
}