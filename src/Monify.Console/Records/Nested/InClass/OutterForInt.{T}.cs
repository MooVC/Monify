namespace Monify.Console.Records.Nested.InClass;

public sealed partial class OutterForInt<T>
    where T : struct
{
    [Monify<int>]
    public sealed partial record Inner;
}