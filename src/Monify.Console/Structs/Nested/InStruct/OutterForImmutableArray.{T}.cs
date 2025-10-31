namespace Monify.Console.Structs.Nested.InStruct;

using System.Collections.Immutable;

/// <summary>
/// Provides a nested struct example that uses Monify with immutable array of string types.
/// </summary>
/// <typeparam name="T">The struct type that the outer struct is parameterized with.</typeparam>
public readonly ref partial struct OutterForImmutableArray<T>
    where T : struct
{
    /// <summary>
    /// Represents the inner struct decorated by Monify for immutable array of string types.
    /// </summary>
    [Monify<ImmutableArray<string>>]
    public readonly partial struct Inner;
}