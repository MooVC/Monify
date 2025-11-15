namespace Monify.Model;

using System.Collections.Immutable;
using Valuify;

/// <summary>
/// The definition of the <see cref="Subject"/> type, which is used to capture information relating to a subject
/// upon which the Monify attribute has been placed.
/// </summary>
[Valuify]
internal sealed partial class Subject
{
    /// <summary>
    /// Gets or sets a value indicating whether or not the subject can override <see cref="object.Equals(object)"/>.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject can override <see cref="object.Equals(object)"/>.
    /// </value>
    public bool CanOverrideEquals { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the subject can override <see cref="object.GetHashCode()"/>.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject can override <see cref="object.GetHashCode()"/>.
    /// </value>
    public bool CanOverrideGetHashCode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the subject can override <see cref="object.ToString()"/>.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject can override <see cref="object.ToString()"/>.
    /// </value>
    public bool CanOverrideToString { get; set; }

    /// <summary>
    /// Gets or sets the conversions that should be generated in addition to the conversion for the encapsulated value.
    /// </summary>
    /// <value>
    /// The additional conversions that should be generated.
    /// </value>
    public ImmutableArray<Conversion> Conversions { get; set; } = ImmutableArray<Conversion>.Empty;

    /// <summary>
    /// Gets or sets the type declaration of the subject, be it a class, record or struct.
    /// </summary>
    /// <value>
    /// The type declaration of the subject, be it a class, record or struct.
    /// </value>
    public string Declaration { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether or not the subject defines a constructor for the encapsulated value.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject defines a constructor for the encapsulated value.
    /// </value>
    public bool HasConstructorForEncapsulatedValue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the subject implements <see cref="IEquatable{T}.Equals(T)"/> for its own type.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject implements <see cref="IEquatable{T}.Equals(T)"/> for its own type.
    /// </value>
    public bool HasEquatableForSelf { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the subject implements <see cref="IEquatable{T}.Equals(T)"/> for the encapsulated value.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject implements <see cref="IEquatable{T}.Equals(T)"/> for the encapsulated value.
    /// </value>
    public bool HasEquatableForValue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the subject declares an equality operator for its own type.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject declares an equality operator for its own type.
    /// </value>
    public bool HasEqualityOperatorForSelf { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the subject declares an equality operator for the encapsulated value.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject declares an equality operator for the encapsulated value.
    /// </value>
    public bool HasEqualityOperatorForValue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the subject defines a field for the encapsulated value.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject defines a field for the encapsulated value.
    /// </value>
    public bool HasFieldForEncapsulatedValue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the subject declares an inequality operator for its own type.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject declares an inequality operator for its own type.
    /// </value>
    public bool HasInequalityOperatorForSelf { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the subject declares an inequality operator for the encapsulated value.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject declares an inequality operator for the encapsulated value.
    /// </value>
    public bool HasInequalityOperatorForValue { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the subject implements <see cref="IEquatable{T}"/> for its own type.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject implements <see cref="IEquatable{T}"/>for its own type.
    /// </value>
    public bool IsEquatableToSelf { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the subject implements <see cref="IEquatable{T}"/> for the encapsulated value.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject implements <see cref="IEquatable{T}"/> for the encapsulated value.
    /// </value>
    public bool IsEquatableToValue { get; set; }

    /// <summary>
    /// Gets a value indicating whether or not the subject belongs to the global namespace.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the subject belongs to the global namespace.
    /// </value>
    public bool IsGlobal => string.IsNullOrEmpty(Namespace);

    /// <summary>
    /// Gets or sets a value indicating whether or not the encapsulated type is deemed to be a sequence.
    /// </summary>
    /// <value>
    /// The value indicating whether or not the encapsulated type is deemed to be a sequence.
    /// </value>
    public bool IsSequence { get; set; }

    /// <summary>
    /// Gets or sets the name of the subject.
    /// </summary>
    /// <value>
    /// The name of the subject.
    /// </value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the globally qualified namespace for the subject.
    /// </summary>
    /// <value>
    /// The globally qualified namespace for the subject.
    /// </value>
    public string Namespace { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the declarations associated with the parent types in order of declaration.
    /// </summary>
    /// <value>
    /// The declarations associated with the parent types in order of declaration.
    /// </value>
    public ImmutableArray<Nesting> Nesting { get; set; } = ImmutableArray<Nesting>.Empty;

    /// <summary>
    /// Gets or sets the qualified name of the subject, which includes any generic arguments.
    /// </summary>
    /// <value>
    /// The qualified name of the subject, which includes any generic arguments.
    /// </value>
    public string Qualification { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the qualified name of the value that is encapsulated by the subject.
    /// </summary>
    /// <value>
    /// The qualified name of the value that is encapsulated by the subject.
    /// </value>
    public string Value { get; set; } = string.Empty;
}