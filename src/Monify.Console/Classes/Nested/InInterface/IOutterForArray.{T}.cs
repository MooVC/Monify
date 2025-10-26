namespace Monify.Console.Classes.Nested.InInterface;

public partial interface IOutterForArray<T>
    where T : struct
{
    [Monify<int[]>]
    public sealed partial class Inner;
}