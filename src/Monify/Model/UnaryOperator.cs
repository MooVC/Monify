namespace Monify.Model;

using Valuify;

/// <summary>
/// Represents a unary operator to be forwarded from the encapsulated type.
/// </summary>
[Valuify]
internal sealed partial class UnaryOperator
{
    /// <summary>
    /// Gets or sets a value indicating whether the unary operator should return the subject instead of the encapsulated type.
    /// </summary>
    /// <value>
    /// A value indicating whether the unary operator should return the subject instead of the encapsulated type.
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
    /// Gets or sets the unary operator token used in the generated source.
    /// </summary>
    /// <value>
    /// The unary operator token used in the generated source.
    /// </value>
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the converted return type.
    /// </summary>
    /// <value>
    /// The converted return type.
    /// </value>
    public string Return { get; set; } = string.Empty;
}
