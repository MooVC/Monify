namespace Monify.Console.Classes.Nested.InInterface;

/// <summary>
/// Defines a nested interface example that uses Monify with integer types.
/// </summary>
/// <typeparam name="T">The struct type that the interface is parameterized with.</typeparam>
public partial interface IOutterForInt<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner class decorated by Monify for integer types.
    /// </summary>
    [Monify<int>]
    public sealed partial class Inner;
}