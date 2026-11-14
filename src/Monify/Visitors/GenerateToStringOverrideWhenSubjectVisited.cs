namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Graphify;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;

    using static Monify.Visitors.GenerateToStringOverrideWhenSubjectVisited_Resources;

    /// <summary>
    /// Generates the source needed to support <see cref="object.ToString()"/>.
    /// </summary>
    internal sealed class GenerateToStringOverrideWhenSubjectVisited
        : ISubjectVisitor<Subject.Graph, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph instance)
        {
            Subject subject = ((IGraph<Subject>)instance).Root;

            if (!subject.CanOverrideToString)
            {
                yield break;
            }

            Method method = Method.Undefined
                .Named(nameof(ToString))
                .WithExtensibility(Modifiers.Override)
                .Returns(Result.Void.OfType(typeof(string)))
                .WithBody(Snippet.From(Configuration.Options, string.Format(ToStringSource, GenerateValueFieldWhenEncapsulatedVisited.Name)));

            yield return subject.ToSource(nameof(ToString), method);
        }
    }
}