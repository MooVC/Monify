namespace Monify.Syntax
{
    using System.Linq;
    using Monify.Model;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;
    using Attribute = MooVC.Syntax.CSharp.Attribute;

    internal static class GenerationSyntax
    {
        private static readonly Options _options = new Options()
            .WithNamespace(Qualifier.Options.Block)
            .WithTypes(types => types.WithQualifications(qualifications => qualifications
                .WithFormat(Qualification.Options.Formats.Global)));

        public static Snippet Block(Snippet body, string declaration)
        {
            return ToSnippet(body).Block(_options, declaration);
        }

        public static Source CreateInterfaceSource(Subject subject, string hint, string contract, params Attribute[] attributes)
        {
            string declaration = $"{subject.Declaration} {subject.Qualification} : {contract}";

            return new Source(WithAttributes(Block(Snippet.Empty, declaration), attributes), hint);
        }

        public static Source CreateSource(Subject subject, string hint, Method member, params Attribute[] attributes)
        {
            return CreateSource(subject, hint, member.ToSnippet(_options.Types), attributes);
        }

        public static Source CreateSource(Subject subject, string hint, Snippet member, params Attribute[] attributes)
        {
            return new Source(WithAttributes(Declare(subject, member), attributes), hint);
        }

        public static string Self(Subject subject)
        {
            return string.IsNullOrEmpty(subject.QualifiedName) ? subject.Qualification : subject.QualifiedName;
        }

        public static Snippet ToSnippet(string text)
        {
            return Snippet.From(text.NormalizeLineEndings().Split('\n'));
        }

        private static Snippet Declare(Subject subject, Snippet member)
        {
            return Block(member, $"{subject.Declaration} {subject.Qualification}");
        }

        private static Snippet WithAttributes(Snippet declaration, Attribute[] attributes)
        {
            return declaration.Prepend(_options, attributes.Select(attribute => Snippet.From($"[{attribute.ToSnippet(_options.Types)}]")).ToArray());
        }
    }
}