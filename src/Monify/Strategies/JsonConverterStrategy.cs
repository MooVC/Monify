namespace Monify.Strategies
{
    using System.Collections.Generic;
    using Monify.Model;

    using static Monify.Strategies.JsonConverterStrategy_Resources;

    /// <summary>
    /// Generates a JSON converter that serializes the encapsulated value.
    /// </summary>
    internal sealed class JsonConverterStrategy
        : IStrategy
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Generate(Subject subject)
        {
            Serialization serialization = subject.Serialization;

            if (serialization.HasJsonConverter || !serialization.SupportsJsonSerialization)
            {
                yield break;
            }

            string converter = subject.Qualification + ".Converter";

            if (!string.IsNullOrEmpty(serialization.JsonConverterFactoryName))
            {
                converter = subject.IsGlobal
                    ? "global::" + serialization.JsonConverterFactoryName
                    : "global::" + subject.Namespace + "." + serialization.JsonConverterFactoryName;

                string factory = string.Format(FactorySource, serialization.JsonConverterFactoryName, serialization.JsonConverterFactoryTarget);

                yield return new Source(factory, "JsonConverterFactory", isNested: false);
            }

            string code = string.Format(ConverterSource, subject.Declaration, subject.Qualification, subject.Value, FieldStrategy.Name, converter);

            yield return new Source(code, "JsonConverter");
        }
    }
}