namespace Monify.Syntax
{
    using Monify.Model;
    using MooVC.Syntax;

    using static Monify.Syntax.InequalityOperatorSyntax_Resources;

    /// <summary>
    /// Renders the shared inequality operator implementation.
    /// </summary>
    internal static class InequalityOperatorSyntax
    {
        public static Source Create(Subject subject, string hint, string rightType)
        {
            string declaration = $"public static bool operator !=({GenerationSyntax.Self(subject)} left, {rightType} right)";
            Snippet body = Snippet.From(InequalitySource);

            return GenerationSyntax.CreateSource(subject, hint, GenerationSyntax.Block(body, declaration));
        }
    }
}