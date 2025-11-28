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
    /// <value>
    /// A value indicating whether the conversion should replace the encapsulated type with the subject parameter.
    /// </value>
    public bool IsParameterSubject { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the conversion should return the subject instead of the encapsulated type.
    /// </summary>
    /// <value>
    /// A value indicating whether the conversion should return the subject instead of the encapsulated type.
    /// </value>
    public bool IsReturnSubject { get; set; }

    /// <summary>
    /// Gets or sets the name of the operator being forwarded.
    /// </summary>
    /// <value>
    /// The name of the operator being forwarded.
    /// </value>
    public string Operator { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the converted parameter type.
    /// </summary>
    /// <value>
    /// The converted parameter type.
    /// </value>
    public string Parameter { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the converted return type.
    /// </summary>
    /// <value>
    /// The converted return type.
    /// </value>
    public string Return { get; set; } = string.Empty;
}