namespace Monify.Model
{
    using System.Collections.Immutable;
    using Fluentify;
    using MooVC.Syntax.CSharp;
    using Valuify;

    /// <summary>
    /// Represents a method that can be forwarded to the encapsulated value.
    /// </summary>
    [Fluentify]
    [Valuify]
    internal sealed partial class PassthroughMethod
    {
        /// <summary>
        /// Gets or sets the accessibility used when declaring the method.
        /// </summary>
        /// <value>
        /// The accessibility used when declaring the method.
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
        /// Gets a value indicating whether or not the method is an explicit interface implementation.
        /// </summary>
        /// <value>
        /// A value indicating whether or not the method is an explicit interface implementation.
        /// </value>
        public bool IsExplicit => ExplicitInterface.Length > 0;

        /// <summary>
        /// Gets or sets the method name.
        /// </summary>
        /// <value>
        /// The method name.
        /// </value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the method parameters.
        /// </summary>
        /// <value>
        /// The method parameters.
        /// </value>
        public ImmutableArray<PassthroughParameter> Parameters { get; set; } = ImmutableArray<PassthroughParameter>.Empty;

        /// <summary>
        /// Gets or sets the method return type.
        /// </summary>
        /// <value>
        /// The method return type.
        /// </value>
        public string Return { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the method type parameters.
        /// </summary>
        /// <value>
        /// The method type parameters.
        /// </value>
        public ImmutableArray<Generic> TypeParameters { get; set; } = ImmutableArray<Generic>.Empty;
    }
}