namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Graphify;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;

    using static Monify.Visitors.GenerateEqualsOverrideWhenSubjectVisited_Resources;

    /// <summary>
    /// Generates the source needed to support <see cref="object.Equals(object)"/>.
    /// </summary>
    internal sealed class GenerateEqualsOverrideWhenSubjectVisited
        : ISubjectVisitor<Subject.Graph, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph instance)
        {
            Subject subject = ((IGraph<Subject>)instance).Root;

            if (!subject.CanOverrideEquals)
            {
                yield break;
            }

            Method method = Method.Undefined
                .Named(nameof(Equals))
                .WithExtensibility(Modifiers.Override)
                .Accepts(parameter => parameter.Named("Other").OfType(typeof(object)))
                .Returns(Result.Void.OfType(typeof(bool)))
                .WithBody(Snippet.From(Configuration.Options, string.Format(EqualsSource, subject.GetSelfType())));

            yield return subject.ToSource(nameof(Equals), method);
        }
    }
}