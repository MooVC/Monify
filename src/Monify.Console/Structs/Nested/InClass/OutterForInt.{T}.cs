namespace Monify.Testing.Structs.Nested.InClass;

public sealed partial class OutterForInt<T>
    where T : struct
{
    [Monify<int>]
    public readonly partial struct Inner;
}