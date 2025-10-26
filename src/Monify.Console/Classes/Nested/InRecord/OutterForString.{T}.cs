namespace Monify.Testing.Classes.Nested.InRecord;

public sealed partial record OutterForString<T>
    where T : struct
{
    [Monify<string>]
    public sealed partial class Inner
    {
    }
}