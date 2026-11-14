namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;

    using static Monify.Visitors.GenerateJsonConverterWhenSerializationVisited_Resources;
    using Attribute = MooVC.Syntax.CSharp.Attribute;
    using Type = System.Type;

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

            string self = subject.GetSelfType();
            string converter = self + ".Converter";

            if (!string.IsNullOrEmpty(serialization.JsonConverterFactoryName))
            {
                converter = subject.IsGlobal
                    ? "global::" + serialization.JsonConverterFactoryName
                    : "global::" + subject.Namespace + "." + serialization.JsonConverterFactoryName;

                yield return new Source(CreateFactory(serialization), "JsonConverterFactory", isNested: false);
            }

            Attribute attribute = Attribute.Unspecified
                .Named((Name: "JsonConverter", Qualifier: "System.Text.Json.Serialization"))
                .WithArguments((Name: string.Empty, Value: $"typeof({converter})"));

            yield return subject.ToSource("JsonConverter", CreateConverter(subject, self), attribute);
        }

        private static Snippet CreateConverter(Subject subject, string self)
        {
            Symbol wrapper = TypeReferences.FromRoslyn(self);
            Symbol options = (Name: "JsonSerializerOptions", Qualifier: "System.Text.Json");
            Symbol reader = (Name: "Utf8JsonReader", Qualifier: "System.Text.Json");
            Symbol writer = (Name: "Utf8JsonWriter", Qualifier: "System.Text.Json");

            return Class.Undefined
                .Named("Converter")
                .WithScope(Scopes.Public)
                .WithExtensibility(Modifiers.Sealed)
                .IsPartial(false)
                .DerivesFrom(@base => @base
                    .Named((Name: "JsonConverter", Qualifier: "System.Text.Json.Serialization"))
                    .WithGenerics(wrapper))
                .WithMethods(read => read
                    .Named("Read")
                    .WithExtensibility(Modifiers.Override)
                    .Returns(Result.Void.OfType(wrapper))
                    .Accepts(parameter => parameter.Named("Reader").OfType(reader).WithModifier(Parameter.Modes.Ref))
                    .Accepts(parameter => parameter.Named("TypeToConvert").OfType(typeof(Type)))
                    .Accepts(parameter => parameter.Named("Options").OfType(options))
                    .WithBody(string.Format(ReadBody, self, subject.Value)))
                .WithMethods(write => write
                    .Named("Write")
                    .WithExtensibility(Modifiers.Override)
                    .Returns(Result.Void)
                    .Accepts(parameter => parameter.Named("Writer").OfType(writer))
                    .Accepts(parameter => parameter.Named("Value").OfType(wrapper))
                    .Accepts(parameter => parameter.Named("Options").OfType(options))
                    .WithBody(string.Format(WriteBody, subject.Value, GenerateValueFieldWhenEncapsulatedVisited.Name)))
                .ToSnippet(Configuration.Options);
        }

        private static Snippet CreateFactory(Serialization serialization)
        {
            Symbol converter = (Name: "JsonConverter", Qualifier: "System.Text.Json.Serialization");
            Symbol factory = (Name: "JsonConverterFactory", Qualifier: "System.Text.Json.Serialization");
            Symbol options = (Name: "JsonSerializerOptions", Qualifier: "System.Text.Json");

            return Class.Undefined
                .Named(serialization.JsonConverterFactoryName)
                .WithScope(Scopes.Internal)
                .WithExtensibility(Modifiers.Sealed)
                .IsPartial(false)
                .DerivesFrom(factory)
                .WithMethods(canConvert => canConvert
                    .Named("CanConvert")
                    .WithExtensibility(Modifiers.Override)
                    .Returns(Result.Void.OfType(typeof(bool)))
                    .Accepts(parameter => parameter.Named("TypeToConvert").OfType(typeof(Type)))
                    .WithBody(string.Format(CanConvertBody, serialization.JsonConverterFactoryTarget)))
                .WithMethods(create => create
                    .Named("CreateConverter")
                    .WithExtensibility(Modifiers.Override)
                    .Returns(Result.Void.OfType(converter))
                    .Accepts(parameter => parameter.Named("TypeToConvert").OfType(typeof(Type)))
                    .Accepts(parameter => parameter.Named("Options").OfType(options))
                    .WithBody(Snippet.From(Configuration.Options, CreateConverterBody)))
                .ToSnippet(Configuration.Options);
        }
    }
}