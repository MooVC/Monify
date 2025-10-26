namespace Monify.Testing.Classes.Nested.InRecord;

public sealed partial record OutterForInt<T>
    where T : struct
{
    [Monify<int>]
    public sealed partial class Inner;
}