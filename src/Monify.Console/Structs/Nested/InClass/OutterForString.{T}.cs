namespace Monify.Console.Structs.Nested.InClass;

public sealed partial class OutterForString<T>
    where T : struct
{
    [Monify<string>]
    public readonly partial struct Inner;
}