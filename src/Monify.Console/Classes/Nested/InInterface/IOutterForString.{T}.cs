namespace Monify.Console.Classes.Nested.InInterface;

/// <summary>
/// Defines a nested interface example that uses Monify with string types.
/// </summary>
/// <typeparam name="T">The struct type that the interface is parameterized with.</typeparam>
public partial interface IOutterForString<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner class decorated by Monify for string types.
    /// </summary>
    [Monify<string>]
    public sealed partial class Inner;
}