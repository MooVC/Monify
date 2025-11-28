namespace Monify.Model;

using Valuify;

/// <summary>
/// Represents a conversion operator to be forwarded from the encapsulated type.
/// </summary>
[Valuify]
internal sealed partial class Conversion
{
    /// <summary>
    /// Gets or sets a value indicating whether the conversion should replace the encapsulated type with the subject parameter.
    /// </summary>
    public bool IsParameterSubject { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the conversion should return the subject instead of the encapsulated type.
    /// </summary>
    public bool IsReturnSubject { get; set; }

    /// <summary>
    /// Gets or sets the name of the operator being forwarded.
    /// </summary>
    public string Operator { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the converted parameter type.
    /// </summary>
    public string Parameter { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the converted return type.
    /// </summary>
    public string Return { get; set; } = string.Empty;
}
