namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;

    using static Monify.Model.Subject;
    using static Monify.Visitors.GenerateValueFieldWhenEncapsulatedVisited_Resources;
    using EncapsulatedNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated;

    /// <summary>
    /// Generates the source needed to define the field that holds the state of the encapsulated value.
    /// </summary>
    internal sealed class GenerateValueFieldWhenEncapsulatedVisited
        : ISubjectVisitor<EncapsulatedNode, Source>
    {
        /// <summary>
        /// Defines the name of the field used to store the state of the encapsulared value.
        /// </summary>
        public const string Name = "_value";

        /// <inheritdoc/>
        public IEnumerable<Source> Observe(EncapsulatedNode instance)
        {
            Subject subject = instance._Root;

            if (instance._Index != IndexForEncapsulatedValue || subject.HasField)
            {
                yield break;
            }

            var field = Snippet.From(string.Format(FieldSource, instance._Value.Type, Name));

            yield return GenerationSyntax.CreateSource(subject, Name, field);
        }
    }
}