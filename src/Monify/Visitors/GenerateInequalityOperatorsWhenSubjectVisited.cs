namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;

    /// <summary>
    /// Generates the inequality operator that compares two instances of the subject.
    /// </summary>
    internal sealed class GenerateInequalityOperatorsWhenSubjectVisited
        : ISubjectVisitor<Subject.Graph.Self, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph.Self instance)
        {
            Subject subject = instance._Value;

            if (!subject.HasInequalityOperator)
            {
                yield return InequalityOperatorSyntax.Create(subject, "Inequality.Self", GenerationSyntax.Self(subject));
            }
        }
    }
}