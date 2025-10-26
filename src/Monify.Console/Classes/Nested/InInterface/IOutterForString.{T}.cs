namespace Monify.Console.Classes.Nested.InInterface;

public partial interface IOutterForString<T>
    where T : struct
{
    [Monify<string>]
    public sealed partial class Inner;
}