namespace Monify.Console.Records.Nested.InClass;

public sealed partial class OutterForArray<T>
    where T : struct
{
    [Monify<int[]>]
    public sealed partial record Inner;
}