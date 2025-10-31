namespace Monify.Console.Classes.Nested.InRecord;

using System.Collections.Immutable;

/// <summary>
/// Provides a nested record example that uses Monify with immutable array of string types.
/// </summary>
/// <typeparam name="T">The record type that the outer record is parameterized with.</typeparam>
public sealed partial record OutterForImmutableArray<T>
{
    /// <summary>
    /// Represents the inner class decorated by Monify for immutable array of string types.
    /// </summary>
    [Monify<ImmutableArray<string>>]
    public sealed partial class Inner;
}