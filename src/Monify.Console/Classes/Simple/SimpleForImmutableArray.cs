namespace Monify.Console.Classes.Simple;

using System.Collections.Immutable;

/// <summary>
/// Represents a simple class decorated by Monify for immutable array of string types.
/// </summary>
[Monify<ImmutableArray<string>>]
public partial class SimpleForImmutableArray;