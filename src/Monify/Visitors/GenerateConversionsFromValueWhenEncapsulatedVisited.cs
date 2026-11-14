namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;
    using static Monify.Model.Subject;
    using static Monify.Visitors.GenerateConversionsFromValueWhenEncapsulatedVisited_Resources;
    using Conversion = MooVC.Syntax.CSharp.Conversion;

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

            Conversion member = Conversion.Undefined
                .WithDirection(Conversion.Intents.From)
                .WithInput(parameter => parameter.Named("Value"))
                .ForType(TypeReferences.FromRoslyn(conversion.Type))
                .WithBody(Snippet.From(Configuration.Options, string.Format(ConversionSource, subject.GetSelfType())));

            yield return member.ToSource(subject, hint);
        }
    }
}