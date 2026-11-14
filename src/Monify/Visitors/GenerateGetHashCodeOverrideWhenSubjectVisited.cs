namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Graphify;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;

    using static Monify.Visitors.GenerateGetHashCodeOverrideWhenSubjectVisited_Resources;

    /// <summary>
    /// Generates the source needed to support <see cref="object.GetHashCode()"/>.
    /// </summary>
    internal sealed class GenerateGetHashCodeOverrideWhenSubjectVisited
        : ISubjectVisitor<Subject.Graph, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph instance)
        {
            Subject subject = ((IGraph<Subject>)instance).Root;

            if (!subject.CanOverrideGetHashCode)
            {
                yield break;
            }

            Method method = Method.Undefined
                .Named(nameof(GetHashCode))
                .WithExtensibility(Modifiers.Override)
                .Returns(Result.Void.OfType(typeof(int)))
                .WithBody(Snippet.From(Configuration.Options, string.Format(GetHashCodeSource, GenerateValueFieldWhenEncapsulatedVisited.Name)));

            yield return subject.ToSource(nameof(GetHashCode), method);
        }
    }
}