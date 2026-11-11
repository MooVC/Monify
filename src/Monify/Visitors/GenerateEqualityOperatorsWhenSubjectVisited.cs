namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;

    /// <summary>
    /// Generates the equality operator for two instances of the subject.
    /// </summary>
    internal sealed class GenerateEqualityOperatorsWhenSubjectVisited
        : ISubjectVisitor<Subject.Graph.Self, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph.Self instance)
        {
            Subject subject = instance._Value;

            if (!subject.HasEqualityOperator)
            {
                yield return EqualityOperatorSyntax.Create(subject, "Equality.Self", GenerationSyntax.Self(subject));
            }
        }
    }
}