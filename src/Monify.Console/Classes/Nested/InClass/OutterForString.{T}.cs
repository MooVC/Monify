namespace Monify.Console.Classes.Nested.InClass;

/// <summary>
/// Provides a nested class example that uses Monify with string types.
/// </summary>
/// <typeparam name="T">The struct type that the outer class is parameterized with.</typeparam>
public sealed partial class OutterForString<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner class decorated by Monify for string types.
    /// </summary>
    [Monify<string>]
    public sealed partial class Inner;
}