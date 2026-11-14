namespace Monify.Syntax
{
    using System.Linq;
    using Monify.Model;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;

    internal static partial class SubjectExtensions
    {
        public static Source ToSource(this Subject subject, string hint, Method member, params Attribute[] attributes)
        {
            return subject.ToSource(hint, member.ToSnippet(Configuration.Options), attributes);
        }

        public static Source ToSource(this Subject subject, string hint, Comparison member)
        {
            Class operand = Class.Undefined.Named(subject.GetSelfType());

            return subject.ToSource(hint, member.ToSnippet(Configuration.Options, operand));
        }

        public static Source ToSource(this Subject subject, string hint, Snippet member, params Attribute[] attributes)
        {
            Snippet declaration = member.Block(Configuration.Options, $"{subject.Declaration} {subject.Qualification}");

            return new Source(declaration.Prepend(Configuration.Options, RenderAttributes(attributes)), hint);
        }

        public static Source ToSource(this Subject subject, string hint, Symbol contract, params Attribute[] attributes)
        {
            Snippet declaration;

            switch (subject.Declaration)
            {
                case "sealed partial class":
                    declaration = Class.Undefined.Named(subject.Qualification)
                        .WithScope(Scopes.Unspecified)
                        .WithExtensibility(Modifiers.Sealed)
                        .Implements(contract)
                        .ToSnippet(Configuration.Options);
                    break;
                case "sealed partial record":
                    declaration = Record.Undefined.Named(subject.Qualification)
                        .WithScope(Scopes.Unspecified)
                        .WithExtensibility(Modifiers.Sealed)
                        .Implements(contract)
                        .ToSnippet(Configuration.Options);

                    // A partial implementation needs a body even when no members are declared.
                    declaration = Snippet.Empty.Block(Configuration.Options, Snippet.From(declaration.ToString().TrimEnd(';')));
                    break;
                default:
                    // Struct implementations are not rendered by MooVC 10.0.3. Keep Roslyn's partial declaration.
                    declaration = Snippet.Empty.Block(
                        Configuration.Options,
                        $"{subject.Declaration} {subject.Qualification} : {contract.ToSnippet(Configuration.Options)}");
                    break;
            }

            return new Source(declaration.Prepend(Configuration.Options, RenderAttributes(attributes)), hint);
        }

        public static string GetSelfType(this Subject subject)
        {
            return string.IsNullOrEmpty(subject.QualifiedName) ? subject.Qualification : subject.QualifiedName;
        }

        private static Snippet[] RenderAttributes(Attribute[] attributes)
        {
            return attributes.Select(attribute => Snippet.From($"[{attribute.ToSnippet(Configuration.Options)}]")).ToArray();
        }
    }
}