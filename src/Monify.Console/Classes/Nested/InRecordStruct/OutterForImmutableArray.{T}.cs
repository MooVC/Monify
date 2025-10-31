namespace Monify.Console.Classes.Nested.InRecordStruct;

using System.Collections.Immutable;

/// <summary>
/// Provides a nested record struct example that uses Monify with immutable array of string types.
/// </summary>
/// <typeparam name="T">The record struct type that the outer record struct is parameterized with.</typeparam>
public readonly partial record struct OutterForImmutableArray<T>
{
    /// <summary>
    /// Represents the inner class decorated by Monify for immutable array of string types.
    /// </summary>
    [Monify<ImmutableArray<string>>]
    public sealed partial class Inner;
}