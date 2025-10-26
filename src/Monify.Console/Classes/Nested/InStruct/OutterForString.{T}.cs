namespace Monify.Console.Classes.Nested.InStruct;

/// <summary>
/// Provides a nested struct example that uses Monify with string types.
/// </summary>
/// <typeparam name="T">The struct type that the outer struct is parameterized with.</typeparam>
public readonly ref partial struct OutterForString<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner class decorated by Monify for string types.
    /// </summary>
    [Monify<string>]
    public sealed partial class Inner;
}