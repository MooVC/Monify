namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax.CSharp;

    using static Monify.Visitors.GenerateGetHashCodeOverrideWhenSubjectVisited_Resources;

    /// <summary>
    /// Generates the source needed to support <see cref="object.GetHashCode()"/>.
    /// </summary>
    internal sealed class GenerateGetHashCodeOverrideWhenSubjectVisited
        : ISubjectVisitor<Subject.Graph.Self, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph.Self instance)
        {
            Subject subject = instance._Value;

            if (!subject.CanOverrideGetHashCode)
            {
                yield break;
            }

            Method method = Method.Undefined
                .Named(nameof(GetHashCode))
                .WithExtensibility(Modifiers.Override)
                .Returns(Result.Void.OfType(typeof(int)))
                .WithBody(GenerationSyntax.ToSnippet(string.Format(GetHashCodeSource, GenerateValueFieldWhenEncapsulatedVisited.Name)));

            yield return GenerationSyntax.CreateSource(subject, nameof(GetHashCode), method);
        }
    }
}