namespace Monify.Syntax
{
    using Monify.Model;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;

    using static Monify.Syntax.EqualityOperatorSyntax_Resources;

    internal static class EqualityOperatorSyntax
    {
        public static Source Create(Subject subject, string hint, string rightType)
        {
            Comparison comparison = Comparison.Undefined
                .WithOperator(Comparison.Types.Equality)
                .To(TypeReferences.FromRoslyn(rightType))
                .WithBody(Snippet.From(Configuration.Options, EqualitySource));

            return subject.ToSource(hint, comparison);
        }
    }
}