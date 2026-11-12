namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax.CSharp;

    using static Monify.Visitors.GenerateToStringOverrideWhenSubjectVisited_Resources;

    /// <summary>
    /// Generates the source needed to support <see cref="object.ToString()"/>.
    /// </summary>
    internal sealed class GenerateToStringOverrideWhenSubjectVisited
        : ISubjectVisitor<Subject.Graph.Self, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph.Self instance)
        {
            Subject subject = instance._Value;

            if (!subject.CanOverrideToString)
            {
                yield break;
            }

            Method method = Method.Undefined
                .Named(nameof(ToString))
                .WithExtensibility(Modifiers.Override)
                .Returns(Result.Void.OfType(typeof(string)))
                .WithBody(GenerationSyntax.ToSnippet(string.Format(ToStringSource, GenerateValueFieldWhenEncapsulatedVisited.Name)));

            yield return GenerationSyntax.CreateSource(subject, nameof(ToString), method);
        }
    }
}