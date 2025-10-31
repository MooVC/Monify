namespace Monify.Console.Classes.Nested.InClass;

/// <summary>
/// Provides a nested class example that uses Monify with integer array types.
/// </summary>
/// <typeparam name="T">The struct type that the outer class is parameterized with.</typeparam>
public sealed partial class OutterForArray<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner class decorated by Monify for integer array types.
    /// </summary>
    [Monify<int[]>]
    public sealed partial class Inner;
}