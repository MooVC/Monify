namespace Monify.Console.Classes.Nested.InInterface;

/// <summary>
/// Defines a nested interface example that uses Monify with integer array types.
/// </summary>
/// <typeparam name="T">The struct type that the interface is parameterized with.</typeparam>
public partial interface IOutterForArray<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner class decorated by Monify for integer array types.
    /// </summary>
    [Monify<int[]>]
    public sealed partial class Inner;
}