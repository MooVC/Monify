namespace Monify.Syntax
{
    using Monify.Model;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;

    using static Monify.Syntax.InequalityOperatorSyntax_Resources;

    /// <summary>
    /// Renders the shared inequality operator implementation.
    /// </summary>
    internal static class InequalityOperatorSyntax
    {
        public static Source Create(Subject subject, string hint, string rightType)
        {
            Comparison comparison = Comparison.Undefined
                .WithOperator(Comparison.Types.Inequality)
                .To(TypeReferences.FromRoslyn(rightType))
                .WithBody(Snippet.From(Configuration.Options, InequalitySource));

            return subject.ToSource(hint, comparison);
        }
    }
}