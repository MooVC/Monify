namespace Monify.Console.Records.Simple;

using System.Collections.Immutable;

/// <summary>
/// Represents a simple record decorated by Monify for immutable array of string types.
/// </summary>
[Monify<ImmutableArray<string>>]
public partial record SimpleForImmutableArray;