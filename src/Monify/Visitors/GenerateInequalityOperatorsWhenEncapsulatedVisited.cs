namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;

    /// <summary>
    /// Generates the inequality operator that compares the subject with an encapsulated value.
    /// </summary>
    internal sealed class GenerateInequalityOperatorsWhenEncapsulatedVisited
        : ISubjectVisitor<Subject.Graph.EncapsulatedValues.Encapsulated, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph.EncapsulatedValues.Encapsulated instance)
        {
            Encapsulated encapsulated = instance._Value;

            if (encapsulated.HasInequalityOperator)
            {
                yield break;
            }

            string hint = instance._Index == Subject.IndexForEncapsulatedValue
                ? "Inequality.Value"
                : $"Inequality.Passthrough.Level{instance._Index:D2}";

            yield return InequalityOperatorSyntax.Create(instance._Root, hint, encapsulated.Type);
        }
    }
}