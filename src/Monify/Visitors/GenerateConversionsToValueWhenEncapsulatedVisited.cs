namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;
    using static Monify.Model.Subject;
    using static Monify.Visitors.GenerateConversionsToValueWhenEncapsulatedVisited_Resources;

    /// <summary>
    /// Generates an implicit conversion from the subject to the encapsulated type.
    /// </summary>
    internal sealed class GenerateConversionsToValueWhenEncapsulatedVisited
        : ISubjectVisitor<Subject.Graph.EncapsulatedValues.Encapsulated, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph.EncapsulatedValues.Encapsulated instance)
        {
            Encapsulated conversion = instance._Value;

            if (conversion.HasConversionFrom)
            {
                yield break;
            }

            string hint = instance._Index == IndexForEncapsulatedValue
                ? "ConvertFrom"
                : $"ConvertFrom.Passthrough.Level{instance._Index:D2}";

            Subject subject = instance._Root;
            string declaration = $"public static implicit operator {conversion.Type}({GenerationSyntax.Self(subject)} subject)";
            var body = GenerationSyntax.ToSnippet(ConversionSource);

            yield return GenerationSyntax.CreateSource(subject, hint, GenerationSyntax.Block(body, declaration));
        }
    }
}