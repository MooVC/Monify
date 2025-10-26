namespace Monify.Console.Classes.Nested.InRecordStruct;

/// <summary>
/// Provides a nested record struct example that uses Monify with integer array types.
/// </summary>
/// <typeparam name="T">The struct type that the outer record struct is parameterized with.</typeparam>
public readonly partial record struct OutterForArray<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner class decorated by Monify for integer array types.
    /// </summary>
    [Monify<int[]>]
    public sealed partial class Inner;
}