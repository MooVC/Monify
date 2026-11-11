namespace Monify.Syntax
{
    using Monify.Model;
    using MooVC.Syntax;

    using static Monify.Syntax.EqualityOperatorSyntax_Resources;

    internal static class EqualityOperatorSyntax
    {
        public static Source Create(Subject subject, string hint, string rightType)
        {
            string declaration = $"public static bool operator ==({GenerationSyntax.Self(subject)} left, {rightType} right)";
            Snippet body = GenerationSyntax.ToSnippet(EqualitySource);

            return GenerationSyntax.CreateSource(subject, hint, GenerationSyntax.Block(body, declaration));
        }
    }
}