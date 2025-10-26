namespace Monify.Testing.Structs.Nested.InInterface;

public partial interface IOutterForArray<T>
    where T : struct
{
    [Monify<int[]>]
    public readonly partial struct Inner;
}