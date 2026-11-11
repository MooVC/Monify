namespace Monify.Visitors
{
    using System;
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using static Monify.Model.Subject;

    using static Monify.Syntax.EquatableMemberSyntax_Resources;

    /// <summary>
    /// Generates equatable members for comparison with the encapsulated type.
    /// </summary>
    internal sealed class GenerateEquatableMembersWhenEncapsulatedVisited
        : ISubjectVisitor<Subject.Graph.EncapsulatedValues.Encapsulated, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph.EncapsulatedValues.Encapsulated instance)
        {
            Encapsulated encapsulated = instance._Value;
            string hint = instance._Index == IndexForEncapsulatedValue
                ? "Value"
                : $"Passthrough.{instance._Index:D2}";

            return EquatableMemberSyntax.Create(
                instance._Root,
                encapsulated.Type,
                hint,
                encapsulated.IsEquatable,
                encapsulated.HasEquatable,
                () => GetComparison(encapsulated));
        }

        private static string GetComparison(Encapsulated encapsulated)
        {
            if (encapsulated.IsSequence)
            {
                string check = string.Format(SequenceEqualitySource, GenerateValueFieldWhenEncapsulatedVisited.Name);

                if (IsImmutableArray(encapsulated.Type))
                {
                    check = string.Format(ImmutableArrayEqualitySource, GenerateValueFieldWhenEncapsulatedVisited.Name, check);
                }

                return check;
            }

            return string.Format(EqualityComparerSource, GenerateValueFieldWhenEncapsulatedVisited.Name);
        }

        private static bool IsImmutableArray(string value)
        {
            return value.StartsWith("global::System.Collections.Immutable.ImmutableArray<", StringComparison.Ordinal);
        }
    }
}