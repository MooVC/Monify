namespace Monify.Model
{
    using System.Collections.Immutable;
    using Valuify;

    /// <summary>
    /// Represents a property that can be forwarded to the encapsulated value.
    /// </summary>
    [Valuify]
    internal sealed partial class PassthroughProperty
    {
        /// <summary>
        /// Gets or sets the accessibility used when declaring the property.
        /// </summary>
        /// <value>
        /// The accessibility used when declaring the property.
        /// </value>
        public string Accessibility { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the explicitly implemented interface, when applicable.
        /// </summary>
        /// <value>
        /// The explicitly implemented interface, when applicable.
        /// </value>
        public string ExplicitInterface { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the accessibility modifier used when declaring the getter.
        /// </summary>
        /// <value>
        /// The accessibility modifier used when declaring the getter.
        /// </value>
        public string GetterAccessibility { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether or not the property exposes a getter.
        /// </summary>
        /// <value>
        /// A value indicating whether or not the property exposes a getter.
        /// </value>
        public bool HasGetter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the property exposes a setter.
        /// </summary>
        /// <value>
        /// A value indicating whether or not the property exposes a setter.
        /// </value>
        public bool HasSetter { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the property is an explicit interface implementation.
        /// </summary>
        /// <value>
        /// A value indicating whether or not the property is an explicit interface implementation.
        /// </value>
        public bool IsExplicit => ExplicitInterface.Length > 0;

        /// <summary>
        /// Gets or sets a value indicating whether or not the property is an indexer.
        /// </summary>
        /// <value>
        /// A value indicating whether or not the property is an indexer.
        /// </value>
        public bool IsIndexer { get; set; }

        /// <summary>
        /// Gets or sets the property name.
        /// </summary>
        /// <value>
        /// The property name.
        /// </value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the property parameters.
        /// </summary>
        /// <value>
        /// The property parameters.
        /// </value>
        public ImmutableArray<PassthroughParameter> Parameters { get; set; } = ImmutableArray<PassthroughParameter>.Empty;

        /// <summary>
        /// Gets or sets the accessibility modifier used when declaring the setter.
        /// </summary>
        /// <value>
        /// The accessibility modifier used when declaring the setter.
        /// </value>
        public string SetterAccessibility { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the property type.
        /// </summary>
        /// <value>
        /// The property type.
        /// </value>
        public string Type { get; set; } = string.Empty;
    }
}