namespace Monify.Console.Records.Passthrough;

/// <summary>
/// Represents the outermost wrapper that encapsulates <see cref="Inner"/>.
/// </summary>
[Monify<Inner>]
public partial record Outer;