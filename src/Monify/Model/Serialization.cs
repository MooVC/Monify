namespace Monify.Model
{
    using Fluentify;
    using Valuify;

    /// <summary>
    /// Represents the serialization metadata associated with a subject.
    /// </summary>
    [Fluentify]
    [Valuify]
    internal sealed partial class Serialization
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not the subject is annotated with a JSON converter attribute.
        /// </summary>
        /// <value>
        /// A value indicating whether or not the subject is annotated with a JSON converter attribute.
        /// </value>
        public bool HasJsonConverter { get; set; }

        /// <summary>
        /// Gets or sets the name of the converter factory required for a generic subject.
        /// </summary>
        /// <value>
        /// The converter factory name, or an empty string for a non-generic subject.
        /// </value>
        public string JsonConverterFactoryName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the fully qualified metadata name of the generic subject.
        /// </summary>
        /// <value>
        /// The metadata name used by the converter factory.
        /// </value>
        public string JsonConverterFactoryTarget { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether or not the compilation supports JSON serialization for the subject.
        /// </summary>
        /// <value>
        /// A value indicating whether or not the compilation supports JSON serialization for the subject.
        /// </value>
        public bool SupportsJsonSerialization { get; set; }
    }
}