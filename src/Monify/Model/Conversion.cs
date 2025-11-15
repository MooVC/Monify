namespace Monify.Model;

/// <summary>
/// Represents the metadata needed to determine if an additional conversion should be generated.
/// </summary>
internal sealed class Conversion
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
    /// Gets or sets the fully qualified name of the type that should have conversions generated.
    /// </summary>
    public string Type { get; set; } = string.Empty;
}