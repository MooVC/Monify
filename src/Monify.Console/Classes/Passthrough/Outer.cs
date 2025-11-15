namespace Monify.Console.Classes.Passthrough;
/// <summary>
/// Represents the outermost wrapper that encapsulates <see cref="Inner"/>.
/// </summary>
[Monify<Inner>]
public partial class Outer;