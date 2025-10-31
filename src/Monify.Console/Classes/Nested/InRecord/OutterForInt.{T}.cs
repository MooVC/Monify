namespace Monify.Console.Classes.Nested.InRecord;

/// <summary>
/// Provides a nested record example that uses Monify with integer types.
/// </summary>
/// <typeparam name="T">The struct type that the outer record is parameterized with.</typeparam>
public sealed partial record OutterForInt<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner class decorated by Monify for integer types.
    /// </summary>
    [Monify<int>]
    public sealed partial class Inner;
}