namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Graphify;
    using Monify.Model;
    using Monify.Syntax;

    /// <summary>
    /// Generates the equality operator for two instances of the subject.
    /// </summary>
    internal sealed class GenerateEqualityOperatorsWhenSubjectVisited
        : ISubjectVisitor<Subject.Graph, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph instance)
        {
            Subject subject = ((IGraph<Subject>)instance).Root;

            if (!subject.HasEqualityOperator)
            {
                yield return EqualityOperatorSyntax.Create(subject, "Equality.Self", subject.GetSelfType());
            }
        }
    }
}