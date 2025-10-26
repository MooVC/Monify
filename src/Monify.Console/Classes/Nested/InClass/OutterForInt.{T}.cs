namespace Monify.Console.Classes.Nested.InClass;

public sealed partial class OutterForInt<T>
    where T : struct
{
    [Monify<int>]
    public sealed partial class Inner;
}