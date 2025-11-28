namespace Monify.Model;

using System.Collections.Immutable;
using Valuify;

/// <summary>
/// Represents the metadata associated with the encapsulated type.
/// </summary>
[Valuify]
internal sealed partial class Encapsulated
{
    /// <summary>
    /// Gets or sets the conversions supported by the encapsulated type.
    /// </summary>
    /// <value>
    /// The conversions supported by the encapsulated type.
    /// </value>
    public ImmutableArray<Conversion> Conversions { get; set; } = ImmutableArray<Conversion>.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether or not the subject defines a constructor for the encapsulated value.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject defines a constructor for the encapsulated value.
    /// </value>
    public bool HasConstructor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a conversion from the subject to <see cref="Type"/> already exists.
    /// </summary>
    /// <value>
    /// A value indicating whether a conversion from the subject to <see cref="Type"/> already exists.
    /// </value>
    public bool HasConversionFrom { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a conversion from <see cref="Type"/> to the subject already exists.
    /// </summary>
    /// <value>
    /// A value indicating whether a conversion from <see cref="Type"/> to the subject already exists.
    /// </value>
    public bool HasConversionTo { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the subject already defines an equality operator for <see cref="Type"/>.
    /// </summary>
    /// <value>
    /// A value indicating whether the subject already defines an equality operator for <see cref="Type"/>.
    /// </value>
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
    /// <value>
    /// A value indicating whether the subject already defines an inequality operator for <see cref="Type"/>.
    /// </value>
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
    /// <value>
    /// The fully qualified name of the related type these operator checks apply to.
    /// </value>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the binary operators supported by the encapsulated type.
    /// </summary>
    /// <value>
    /// The binary operators supported by the encapsulated type.
    /// </value>
    public ImmutableArray<BinaryOperator> BinaryOperators { get; set; } = ImmutableArray<BinaryOperator>.Empty;

    /// <summary>
    /// Gets or sets the unary operators supported by the encapsulated type.
    /// </summary>
    /// <value>
    /// The unary operators supported by the encapsulated type.
    /// </value>
    public ImmutableArray<UnaryOperator> UnaryOperators { get; set; } = ImmutableArray<UnaryOperator>.Empty;
}