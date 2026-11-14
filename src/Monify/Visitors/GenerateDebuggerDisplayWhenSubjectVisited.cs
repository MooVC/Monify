namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Graphify;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;

    using static Monify.Visitors.GenerateDebuggerDisplayWhenSubjectVisited_Resources;

    /// <summary>
    /// Generates the source needed to support debugger display formatting.
    /// </summary>
    internal sealed class GenerateDebuggerDisplayWhenSubjectVisited
        : ISubjectVisitor<Subject.Graph, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph instance)
        {
            Subject subject = ((IGraph<Subject>)instance).Root;

            if (!subject.GenerateDebuggerDisplay)
            {
                yield break;
            }

            Attribute attribute = Attribute.Unspecified
                .Named((Name: "DebuggerDisplay", Qualifier: "System.Diagnostics"))
                .WithArguments((Name: string.Empty, Value: "\"{GetDebuggerDisplay(),nq}\""));

            Method method = Method.Undefined
                .Named("GetDebuggerDisplay")
                .Returns(Result.Void.OfType(typeof(string)))
                .WithBody(Snippet.From(Configuration.Options, string.Format(DebuggerDisplaySource, subject.Name, GenerateValueFieldWhenEncapsulatedVisited.Name)))
                .WithScope(Scopes.Private);

            yield return subject.ToSource("DebuggerDisplay", method, attribute);
        }
    }
}