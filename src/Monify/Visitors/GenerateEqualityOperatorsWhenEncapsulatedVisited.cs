namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;

    /// <summary>
    /// Generates the equality operator between the subject and an encapsulated value.
    /// </summary>
    internal sealed class GenerateEqualityOperatorsWhenEncapsulatedVisited
        : ISubjectVisitor<Subject.Graph.EncapsulatedValues.Encapsulated, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph.EncapsulatedValues.Encapsulated instance)
        {
            Encapsulated encapsulated = instance._Value;

            if (encapsulated.HasEqualityOperator)
            {
                yield break;
            }

            string hint = instance._Index == Subject.IndexForEncapsulatedValue
                ? "Equality.Value"
                : $"Equality.Passthrough.Level{instance._Index:D2}";

            yield return EqualityOperatorSyntax.Create(instance._Root, hint, encapsulated.Type);
        }
    }
}