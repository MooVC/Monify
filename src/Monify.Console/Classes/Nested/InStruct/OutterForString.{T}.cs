namespace Monify.Testing.Classes.Nested.InStruct;

public readonly ref partial struct OutterForString<T>
    where T : struct
{
    [Monify<string>]
    public sealed partial class Inner;
}