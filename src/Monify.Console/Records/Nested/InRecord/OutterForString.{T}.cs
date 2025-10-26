namespace Monify.Testing.Records.Nested.InRecord;

public sealed partial record OutterForString<T>
    where T : struct
{
    [Monify<string>]
    public sealed partial record Inner
    {
    }
}