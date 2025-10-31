namespace Monify.Console.Records.Nested.InClass;

using System.Collections.Immutable;

/// <summary>
/// Provides a nested class example that uses Monify with immutable array of string types.
/// </summary>
/// <typeparam name="T">The class type that the outer class is parameterized with.</typeparam>
public sealed partial class OutterForImmutableArray<T>
{
    /// <summary>
    /// Represents the inner record decorated by Monify for immutable array of string types.
    /// </summary>
    [Monify<ImmutableArray<string>>]
    public sealed partial record Inner;
}