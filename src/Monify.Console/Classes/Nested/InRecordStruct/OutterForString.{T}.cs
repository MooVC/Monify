namespace Monify.Console.Classes.Nested.InRecordStruct;

/// <summary>
/// Provides a nested record struct example that uses Monify with string types.
/// </summary>
/// <typeparam name="T">The struct type that the outer record struct is parameterized with.</typeparam>
public readonly partial record struct OutterForString<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner class decorated by Monify for string types.
    /// </summary>
    [Monify<string>]
    public sealed partial class Inner;
}