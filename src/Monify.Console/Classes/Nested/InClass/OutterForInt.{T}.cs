namespace Monify.Console.Classes.Nested.InClass;

/// <summary>
/// Provides a nested class example that uses Monify with integer types.
/// </summary>
/// <typeparam name="T">The struct type that the outer class is parameterized with.</typeparam>
public sealed partial class OutterForInt<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner class decorated by Monify for integer types.
    /// </summary>
    [Monify<int>]
    public sealed partial class Inner;
}