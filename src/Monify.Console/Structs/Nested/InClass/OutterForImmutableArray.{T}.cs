namespace Monify.Console.Structs.Nested.InClass;

using System.Collections.Immutable;

/// <summary>
/// Provides a nested class example that uses Monify with immutable array of string types.
/// </summary>
/// <typeparam name="T">The class type that the outer class is parameterized with.</typeparam>
public sealed partial class OutterForImmutableArray<T>
{
    /// <summary>
    /// Represents the inner struct decorated by Monify for immutable array of string types.
    /// </summary>
    [Monify<ImmutableArray<string>>]
    public readonly partial struct Inner;
}