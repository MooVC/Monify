namespace Monify.Testing.Classes.Nested.InRecord;

public sealed partial record OutterForArray<T>
    where T : struct
{
    [Monify<int[]>]
    public sealed partial class Inner;
}