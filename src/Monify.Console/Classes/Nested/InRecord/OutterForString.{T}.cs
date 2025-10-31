namespace Monify.Console.Classes.Nested.InRecord;

/// <summary>
/// Provides a nested record example that uses Monify with string types.
/// </summary>
/// <typeparam name="T">The struct type that the outer record is parameterized with.</typeparam>
public sealed partial record OutterForString<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner class decorated by Monify for string types.
    /// </summary>
    [Monify<string>]
    public sealed partial class Inner;
}