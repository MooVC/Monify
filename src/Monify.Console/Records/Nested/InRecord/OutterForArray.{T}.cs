namespace Monify.Testing.Records.Nested.InRecord;

public sealed partial record OutterForArray<T>
    where T : struct
{
    [Monify<int[]>]
    public sealed partial record Inner;
}