namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;
    using static Monify.Model.Subject;
    using static Monify.Visitors.GenerateConversionsFromValueWhenEncapsulatedVisited_Resources;

    /// <summary>
    /// Generates an implicit conversion from the encapsulated type to the subject.
    /// </summary>
    internal sealed class GenerateConversionsFromValueWhenEncapsulatedVisited
        : ISubjectVisitor<Subject.Graph.EncapsulatedValues.Encapsulated, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph.EncapsulatedValues.Encapsulated instance)
        {
            Encapsulated conversion = instance._Value;

            if (conversion.HasConversionTo)
            {
                yield break;
            }

            string hint = instance._Index == IndexForEncapsulatedValue
                ? "ConvertTo"
                : $"ConvertTo.Passthrough.Level{instance._Index:D2}";

            Subject subject = instance._Root;
            string self = GenerationSyntax.Self(subject);
            string declaration = $"public static implicit operator {self}({conversion.Type} value)";
            var body = Snippet.From(string.Format(ConversionSource, self));

            yield return GenerationSyntax.CreateSource(subject, hint, GenerationSyntax.Block(body, declaration));
        }
    }
}