namespace Monify.Console.Classes.Nested.InInterface;

using System.Collections.Immutable;

/// <summary>
/// Provides a nested interface example that uses Monify with immutable array of string types.
/// </summary>
/// <typeparam name="T">The struct type that the interface is parameterized with.</typeparam>
public partial interface IOutterForImmutableArray<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner class decorated by Monify for immutable array of string types.
    /// </summary>
    [Monify<ImmutableArray<string>>]
    public sealed partial class Inner;
}