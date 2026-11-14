namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Graphify;
    using Monify.Model;
    using Monify.Syntax;

    /// <summary>
    /// Generates the inequality operator that compares two instances of the subject.
    /// </summary>
    internal sealed class GenerateInequalityOperatorsWhenSubjectVisited
        : ISubjectVisitor<Subject.Graph, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph instance)
        {
            Subject subject = ((IGraph<Subject>)instance).Root;

            if (!subject.HasInequalityOperator)
            {
                yield return InequalityOperatorSyntax.Create(subject, "Inequality.Self", subject.GetSelfType());
            }
        }
    }
}