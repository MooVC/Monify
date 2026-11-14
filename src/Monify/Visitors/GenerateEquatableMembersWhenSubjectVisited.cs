namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Graphify;
    using Monify.Model;
    using Monify.Syntax;

    using static Monify.Syntax.EquatableMemberSyntax_Resources;

    /// <summary>
    /// Generates equatable members for comparison with the subject type.
    /// </summary>
    internal sealed class GenerateEquatableMembersWhenSubjectVisited
        : ISubjectVisitor<Subject.Graph, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph instance)
        {
            Subject subject = ((IGraph<Subject>)instance).Root;

            return EquatableMemberSyntax.Create(
                subject,
                subject.GetSelfType(),
                "Self",
                subject.IsEquatable,
                subject.HasEquatable,
                () => string.Format(SubjectEqualitySource, GenerateValueFieldWhenEncapsulatedVisited.Name));
        }
    }
}