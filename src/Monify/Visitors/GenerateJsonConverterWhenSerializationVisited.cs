namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;

    using static Monify.Visitors.GenerateJsonConverterWhenSerializationVisited_Resources;

    /// <summary>
    /// Generates a JSON converter that serializes the encapsulated value.
    /// </summary>
    internal sealed class GenerateJsonConverterWhenSerializationVisited
        : ISubjectVisitor<Subject.Graph.Serialization, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph.Serialization instance)
        {
            Subject subject = instance._Root;
            Serialization serialization = instance._Value;

            if (serialization.HasJsonConverter || !serialization.SupportsJsonSerialization)
            {
                yield break;
            }

            string self = GenerationSyntax.Self(subject);
            string converter = self + ".Converter";

            if (!string.IsNullOrEmpty(serialization.JsonConverterFactoryName))
            {
                converter = subject.IsGlobal
                    ? "global::" + serialization.JsonConverterFactoryName
                    : "global::" + subject.Namespace + "." + serialization.JsonConverterFactoryName;

                string declaration = $"internal sealed class {serialization.JsonConverterFactoryName} : global::System.Text.Json.Serialization.JsonConverterFactory";
                Snippet factory = GenerationSyntax.ToSnippet(string.Format(FactorySource, serialization.JsonConverterFactoryTarget));

                yield return new Source(GenerationSyntax.Block(factory, declaration), "JsonConverterFactory", isNested: false);
            }

            Attribute attribute = Attribute.Unspecified
                .Named((Name: "JsonConverter", Qualifier: "System.Text.Json.Serialization"))
                .WithArguments((Name: string.Empty, Value: $"typeof({converter})"));
            Snippet body = GenerationSyntax.ToSnippet(string.Format(ConverterSource, self, subject.Value, GenerateValueFieldWhenEncapsulatedVisited.Name));
            Snippet implementation = GenerationSyntax.Block(body, $"public sealed class Converter : global::System.Text.Json.Serialization.JsonConverter<{self}>");

            yield return GenerationSyntax.CreateSource(subject, "JsonConverter", implementation, attribute);
        }
    }
}