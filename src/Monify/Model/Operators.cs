namespace Monify.Model;

/// <summary>
/// Represents the operator metadata needed to determine if additional members should be generated for a related type.
/// </summary>
internal sealed class Operators
{
    /// <summary>
    /// Gets or sets a value indicating whether a conversion from the subject to <see cref="Type"/> already exists.
    /// </summary>
    public bool HasConversionFrom { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a conversion from <see cref="Type"/> to the subject already exists.
    /// </summary>
    public bool HasConversionTo { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the subject already defines an equality operator for <see cref="Type"/>.
    /// </summary>
    public bool HasEqualityOperator { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the subject already defines an inequality operator for <see cref="Type"/>.
    /// </summary>
    public bool HasInequalityOperator { get; set; }

    /// <summary>
    /// Gets or sets the fully qualified name of the related type these operator checks apply to.
    /// </summary>
    public string Type { get; set; } = string.Empty;
}