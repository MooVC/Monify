namespace Monify.Model;

using Valuify;

/// <summary>
/// Represents the metadata associated with the encapsulated type.
/// </summary>
[Valuify]
internal sealed partial class Encapsulated
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
    /// Gets or sets a value indicating whether or not the subject implements <see cref="IEquatable{T}.Equals(T)"/> for the encapsulated value.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject implements <see cref="IEquatable{T}.Equals(T)"/> for the encapsulated value.
    /// </value>
    public bool HasEquatable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the subject already defines an inequality operator for <see cref="Type"/>.
    /// </summary>
    public bool HasInequalityOperator { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the subject implements <see cref="IEquatable{T}"/> for the encapsulated value.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject implements <see cref="IEquatable{T}"/> for the encapsulated value.
    /// </value>
    public bool IsEquatable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the encapsulated type is deemed to be a sequence.
    /// </summary>
    /// <value>
    /// The value indicating whether or not the encapsulated type is deemed to be a sequence.
    /// </value>
    public bool IsSequence { get; set; }

    /// <summary>
    /// Gets or sets the fully qualified name of the related type these operator checks apply to.
    /// </summary>
    public string Type { get; set; } = string.Empty;
}