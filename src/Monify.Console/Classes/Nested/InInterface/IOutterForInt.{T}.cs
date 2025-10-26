namespace Monify.Testing.Classes.Nested.InInterface;

public partial interface IOutterForInt<T>
    where T : struct
{
    [Monify<int>]
    public sealed partial class Inner;
}