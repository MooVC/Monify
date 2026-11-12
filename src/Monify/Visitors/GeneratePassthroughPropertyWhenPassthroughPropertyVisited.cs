namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using PropertyNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Properties.PassthroughProperty;

    /// <summary>
    /// Generates a property that forwards access to the encapsulated value.
    /// </summary>
    internal sealed class GeneratePassthroughPropertyWhenPassthroughPropertyVisited
        : ISubjectVisitor<PropertyNode, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(PropertyNode instance)
        {
            if (instance.Properties.Encapsulated._Index == Subject.IndexForEncapsulatedValue)
            {
                yield return PassthroughMemberSyntax.Create(instance._Root, instance._Value);
            }
        }
    }
}