namespace Monify.Model;

using Valuify;

/// <summary>
/// Represents a parameter used by a passthrough member.
/// </summary>
[Valuify]
internal sealed partial class PassthroughParameter
{
    /// <summary>
    /// Gets or sets the modifier used when calling the passthrough member.
    /// </summary>
    /// <value>
    /// The modifier used when calling the passthrough member.
    /// </value>
    public string ArgumentModifier { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the modifier used when declaring the passthrough member.
    /// </summary>
    /// <value>
    /// The modifier used when declaring the passthrough member.
    /// </value>
    public string DeclarationModifier { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the parameter name.
    /// </summary>
    /// <value>
    /// The parameter name.
    /// </value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the parameter type.
    /// </summary>
    /// <value>
    /// The parameter type.
    /// </value>
    public string Type { get; set; } = string.Empty;
}