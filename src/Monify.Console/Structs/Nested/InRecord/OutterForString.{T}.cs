namespace Monify.Console.Structs.Nested.InRecord;

public sealed partial record OutterForString<T>
    where T : struct
{
    [Monify<string>]
    public readonly partial struct Inner;
}