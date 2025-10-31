namespace Monify.Console.Structs.Simple;

using System.Collections.Immutable;

/// <summary>
/// Represents a simple struct decorated by Monify for immutable array of string types.
/// </summary>
[Monify<ImmutableArray<string>>]
public readonly partial struct SimpleForImmutableArray;