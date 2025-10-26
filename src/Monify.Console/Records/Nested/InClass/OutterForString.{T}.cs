namespace Monify.Testing.Records.Nested.InClass;

public sealed partial class OutterForString<T>
    where T : struct
{
    [Monify<string>]
    public sealed partial record Inner;
}