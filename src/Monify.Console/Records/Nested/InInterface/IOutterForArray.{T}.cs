namespace Monify.Console.Records.Nested.InInterface;

public partial interface IOutterForArray<T>
    where T : struct
{
    [Monify<int[]>]
    public sealed partial record Inner;
}