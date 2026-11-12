namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax.CSharp;

    using static Monify.Visitors.GenerateDebuggerDisplayWhenSubjectVisited_Resources;

    /// <summary>
    /// Generates the source needed to support debugger display formatting.
    /// </summary>
    internal sealed class GenerateDebuggerDisplayWhenSubjectVisited
        : ISubjectVisitor<Subject.Graph.Self, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(Subject.Graph.Self instance)
        {
            Subject subject = instance._Value;

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
                .WithBody(GenerationSyntax.ToSnippet(string.Format(DebuggerDisplaySource, subject.Name, GenerateValueFieldWhenEncapsulatedVisited.Name)))
                .WithScope(Scopes.Private);

            yield return GenerationSyntax.CreateSource(subject, "DebuggerDisplay", method, attribute);
        }
    }
}