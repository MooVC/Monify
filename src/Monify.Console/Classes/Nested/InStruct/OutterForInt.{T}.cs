namespace Monify.Console.Classes.Nested.InStruct;

/// <summary>
/// Provides a nested struct example that uses Monify with integer types.
/// </summary>
/// <typeparam name="T">The struct type that the outer struct is parameterized with.</typeparam>
public readonly ref partial struct OutterForInt<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner class decorated by Monify for integer types.
    /// </summary>
    [Monify<int>]
    public sealed partial class Inner;
}