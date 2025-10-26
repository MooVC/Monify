namespace Monify.Console.Classes.Nested.InRecord;

/// <summary>
/// Provides a nested record example that uses Monify with integer array types.
/// </summary>
/// <typeparam name="T">The struct type that the outer record is parameterized with.</typeparam>
public sealed partial record OutterForArray<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner class decorated by Monify for integer array types.
    /// </summary>
    [Monify<int[]>]
    public sealed partial class Inner;
}