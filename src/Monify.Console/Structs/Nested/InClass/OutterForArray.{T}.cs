namespace Monify.Console.Structs.Nested.InClass;

public sealed partial class OutterForArray<T>
    where T : struct
{
    [Monify<int[]>]
    public readonly partial struct Inner;
}