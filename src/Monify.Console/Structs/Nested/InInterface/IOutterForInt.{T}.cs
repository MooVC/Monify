namespace Monify.Console.Structs.Nested.InInterface;

public partial interface IOutterForInt<T>
    where T : struct
{
    [Monify<int>]
    public readonly partial struct Inner;
}