namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MethodNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Methods.PassthroughMethod;

    /// <summary>
    /// Generates a method that forwards calls to the encapsulated value.
    /// </summary>
    internal sealed class GeneratePassthroughMethodWhenPassthroughMethodVisited
        : ISubjectVisitor<MethodNode, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(MethodNode instance)
        {
            if (instance.Methods.Encapsulated._Index == Subject.IndexForEncapsulatedValue)
            {
                yield return PassthroughMemberSyntax.Create(instance._Root, instance._Value);
            }
        }
    }
}