namespace Monify
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Monify.Model;
    using Monify.Strategies;
    using Monify.Syntax;

    using static Monify.TypeGenerator_Resources;

    /// <summary>
    /// Generates source for a type that is annotated with the Monify attribute.
    /// </summary>
    [Generator(LanguageNames.CSharp)]
    public sealed class TypeGenerator
        : IIncrementalGenerator
    {
        private static readonly IStrategy[] _strategies = new IStrategy[]
        {
            new BinaryOperatorStrategy(),
            new ConstructorStrategy(),
            new ConversionOperatorStrategy(),
            new ConvertFromStrategy(),
            new ConvertToStrategy(),
            new EqualityStrategy(),
            new EqualsStrategy(),
            new EquatableStrategy(),
            new FieldStrategy(),
            new DebuggerDisplayStrategy(),
            new GetHashCodeStrategy(),
            new InterfaceDeclarationStrategy(),
            new InequalityStrategy(),
            new MemberPassthroughStrategy(),
            new ToStringStrategy(),
            new UnaryOperatorStrategy(),
        };

        /// <inheritdoc/>
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValuesProvider<TypeDeclarationSyntax> classes = context
                .SyntaxProvider
                .CreateSyntaxProvider(predicate: IsMatch, transform: Transform)
                .Where(record => record is object);

            IncrementalValueProvider<bool> supportsNullableReferenceTypes = context.CompilationProvider
                .Select((compilation, _) => IsNullableReferenceTypesSupported(compilation));

            IncrementalValuesProvider<(Subject Subject, bool SupportsNullableReferenceTypes)> subjects = classes
               .Combine(context.CompilationProvider)
               .Combine(supportsNullableReferenceTypes)
               .Select((match, cancellationToken) => (Parse(match.Left.Left, match.Left.Right, cancellationToken), match.Right));

            context.RegisterSourceOutput(subjects, Generate);
        }

        private static void Generate(SourceProductionContext context, (Subject Subject, bool SupportsNullableReferenceTypes) match)
        {
            if (match.Subject is null)
            {
                return;
            }

#if DEBUG
            var files = new Dictionary<string, string>();
#endif

            foreach (IStrategy strategy in _strategies)
            {
                IEnumerable<Source> sources = strategy.Generate(match.Subject);

                foreach (Source source in sources)
                {
                    string code = Wrap(source.Code, match.Subject, match.SupportsNullableReferenceTypes).NormalizeLineEndings();
                    string hint = GetHint(source, match.Subject);

#if DEBUG
                    files[hint] = code;
#endif

                    context.AddSource(hint, code);
                }
            }
        }

        private static string GetHint(Source source, Subject subject)
        {
            string name = subject.Nesting
                .Select(parent => parent.Name)
                .Concat(new[] { subject.Name })
                .Aggregate(
                    string.Empty,
                    (current, next) => string.IsNullOrEmpty(current)
                        ? next
                        : $"{current}.{next}");

            string separator = source.Hint.StartsWith(".")
                ? string.Empty
                : ".";

            return $"{subject.Namespace}.{name}{separator}{source.Hint}.g.cs";
        }

        private static bool IsMatch(SyntaxNode node, CancellationToken cancellationToken)
        {
            return node is TypeDeclarationSyntax type && type.AttributeLists.Count > 0;
        }

        private static bool IsNullableReferenceTypesSupported(Compilation compilation)
        {
            SyntaxTree syntax = compilation.SyntaxTrees.FirstOrDefault();

            if (syntax is null)
            {
                return false;
            }

            return syntax.Options is CSharpParseOptions options && options.LanguageVersion >= LanguageVersion.CSharp8;
        }

        private static Subject Parse(TypeDeclarationSyntax syntax, Compilation compilation, CancellationToken cancellationToken)
        {
            return syntax.ToSubject(compilation, cancellationToken);
        }

        private static TypeDeclarationSyntax Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            return context.Node as TypeDeclarationSyntax;
        }

        private static string Nest(string code, Subject subject)
        {
            foreach (Nesting parent in subject.Nesting.Reverse())
            {
                code = code.Indent();

                code = string.Format(NestedTypeSource, parent.Declaration, parent.Qualification, code);
            }

            return code;
        }

        private static string Wrap(string code, Subject subject, bool supportsNullableReferenceTypes)
        {
            code = Nest(code, subject);

            if (supportsNullableReferenceTypes)
            {
                code = string.Format(NullableSource, code);
            }

            code = string.Format(UsingsSource, code);

            if (subject.IsGlobal)
            {
                return code;
            }

            return string.Format(NamespaceSource, subject.Namespace, code.Indent());
        }
    }
}