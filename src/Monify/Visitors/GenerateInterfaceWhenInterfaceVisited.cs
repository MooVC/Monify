namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax.CSharp;
    using static Monify.Model.Subject;
    using static Monify.Visitors.GenerateInterfaceWhenInterfaceVisited_Resources;
    using InterfaceNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Interfaces.String;

    /// <summary>
    /// Generates a declaration for an interface supported by the encapsulated type.
    /// </summary>
    internal sealed class GenerateInterfaceWhenInterfaceVisited
        : ISubjectVisitor<InterfaceNode, Source>
    {
        private static readonly Attribute[] _attributes = new Attribute[]
        {
            CreateSuppression("Naming", "CA1710:Identifiers should have correct suffix", NamingJustification),
            CreateSuppression("Design", "CA1036:Override methods on comparable types", ComparableJustification),
            CreateSuppression("Major Code Smell", "S1210:Comparable types should implement comparison operators", ComparableJustification),
        };

        /// <inheritdoc/>
        public IEnumerable<Source> Observe(InterfaceNode instance)
        {
            if (instance.Interfaces.Encapsulated._Index != IndexForEncapsulatedValue)
            {
                yield break;
            }

            string @interface = instance._Value;
            string hint = $"Interfaces.{@interface.NormalizeTypeForHint()}";

            yield return GenerationSyntax.CreateInterfaceSource(instance._Root, hint, @interface, _attributes);
        }

        private static Attribute CreateSuppression(string category, string rule, string justification)
        {
            return Attribute.Unspecified
                .Named((Name: "SuppressMessage", Qualifier: "System.Diagnostics.CodeAnalysis"))
                .WithArguments(
                    (Name: string.Empty, Value: $"\"{category}\""),
                    (Name: string.Empty, Value: $"\"{rule}\""),
                    (Name: "Justification", Value: $"\"{justification}\""));
        }
    }
}