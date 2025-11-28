namespace Monify.Model;

using Valuify;

/// <summary>
/// Represents a binary operator to be forwarded from the encapsulated type.
/// </summary>
[Valuify]
internal sealed partial class BinaryOperator
{
    /// <summary>
    /// Gets or sets a value indicating whether the left operand should be replaced with the subject.
    /// </summary>
    /// <value>
    /// A value indicating whether the left operand should be replaced with the subject.
    /// </value>
    public bool IsLeftSubject { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the right operand should be replaced with the subject.
    /// </summary>
    /// <value>
    /// A value indicating whether the right operand should be replaced with the subject.
    /// </value>
    public bool IsRightSubject { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the binary operator should return the subject instead of the encapsulated type.
    /// </summary>
    /// <value>
    /// A value indicating whether the binary operator should return the subject instead of the encapsulated type.
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
    /// Gets or sets the left operand type used in the generated source.
    /// </summary>
    /// <value>
    /// The left operand type used in the generated source.
    /// </value>
    public string Left { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the right operand type used in the generated source.
    /// </summary>
    /// <value>
    /// The right operand type used in the generated source.
    /// </value>
    public string Right { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the return type used in the generated source.
    /// </summary>
    /// <value>
    /// The return type used in the generated source.
    /// </value>
    public string Return { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the binary operator token used in the generated source.
    /// </summary>
    /// <value>
    /// The binary operator token used in the generated source.
    /// </value>
    public string Symbol { get; set; } = string.Empty;
}