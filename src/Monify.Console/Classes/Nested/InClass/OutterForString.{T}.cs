namespace Monify.Console.Classes.Nested.InClass;

public sealed partial class OutterForString<T>
    where T : struct
{
    [Monify<string>]
    public sealed partial class Inner;
}