namespace Monify.Console.Records.Nested.InInterface;

public partial interface IOutterForString<T>
    where T : struct
{
    [Monify<string>]
    public sealed partial record Inner;
}