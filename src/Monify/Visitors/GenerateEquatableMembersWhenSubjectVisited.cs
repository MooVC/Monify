namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;

    using static Monify.Syntax.EquatableMemberSyntax_Resources;

    /// <summary>
    /// Generates equatable members for comparison with the subject type.
    /// </summary>
    internal sealed class GenerateEquatableMembersWhenSubjectVisited
        : ISubjectVisitor<Subject.Graph.Self, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph.Self instance)
        {
            Subject subject = instance._Value;

            return EquatableMemberSyntax.Create(
                subject,
                GenerationSyntax.Self(subject),
                "Self",
                subject.IsEquatable,
                subject.HasEquatable,
                () => string.Format(SubjectEqualitySource, GenerateValueFieldWhenEncapsulatedVisited.Name));
        }
    }
}