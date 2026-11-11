namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax.CSharp;

    using static Monify.Visitors.GenerateEqualsOverrideWhenSubjectVisited_Resources;

    /// <summary>
    /// Generates the source needed to support <see cref="object.Equals(object)"/>.
    /// </summary>
    internal sealed class GenerateEqualsOverrideWhenSubjectVisited
        : ISubjectVisitor<Subject.Graph.Self, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph.Self instance)
        {
            Subject subject = instance._Value;

            if (!subject.CanOverrideEquals)
            {
                yield break;
            }

            Method method = Method.Undefined
                .Named(nameof(Equals))
                .WithExtensibility(Modifiers.Override)
                .Accepts(parameter => parameter.Named("Other").OfType(typeof(object)))
                .Returns(Result.Void.OfType(typeof(bool)))
                .WithBody(GenerationSyntax.ToSnippet(string.Format(EqualsSource, GenerationSyntax.Self(subject))));

            yield return GenerationSyntax.CreateSource(subject, nameof(Equals), method);
        }
    }
}