namespace Monify.Console.Structs.Passthrough;

/// <summary>
/// Represents the outermost wrapper that encapsulates <see cref="Inner"/>.
/// </summary>
[Monify<Inner>]
public partial struct Outer;