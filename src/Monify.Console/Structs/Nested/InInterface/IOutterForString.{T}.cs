namespace Monify.Console.Structs.Nested.InInterface;

public partial interface IOutterForString<T>
    where T : struct
{
    [Monify<string>]
    public readonly partial struct Inner;
}