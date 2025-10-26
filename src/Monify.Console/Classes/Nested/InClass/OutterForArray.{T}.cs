namespace Monify.Console.Classes.Nested.InClass;

public sealed partial class OutterForArray<T>
    where T : struct
{
    [Monify<int[]>]
    public sealed partial class Inner;
}