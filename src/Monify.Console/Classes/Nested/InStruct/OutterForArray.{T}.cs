namespace Monify.Testing.Classes.Nested.InStruct;

public readonly ref partial struct OutterForArray<T>
    where T : struct
{
    [Monify<int[]>]
    public sealed partial class Inner
    {
    }
}