namespace Monify;

using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Monify.Model;
using Monify.Strategies;
using Monify.Syntax;

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
        new GetHashCodeStrategy(),
        new InequalityStrategy(),
        new ToStringStrategy(),
        new UnaryOperatorStrategy(),
    };

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<TypeDeclarationSyntax?> classes = context
            .SyntaxProvider
            .CreateSyntaxProvider(predicate: IsMatch, transform: Transform)
            .Where(record => record is not null);

        IncrementalValueProvider<bool> supportsNullableReferenceTypes = context.CompilationProvider
            .Select(static (compilation, _) => IsNullableReferenceTypesSupported(compilation));

        IncrementalValuesProvider<(Subject? Subject, bool SupportsNullableReferenceTypes)> subjects = classes
           .Combine(context.CompilationProvider)
           .Combine(supportsNullableReferenceTypes)
           .Select(static (match, cancellationToken) => (Parse(match.Left.Left, match.Left.Right, cancellationToken), match.Right));

        context.RegisterSourceOutput(subjects, Generate);
    }

    private static void Generate(SourceProductionContext context, (Subject? Subject, bool SupportsNullableReferenceTypes) match)
    {
        if (match.Subject is null)
        {
            return;
        }

#if DEBUG
        Dictionary<string, string> files = new();
#endif

        foreach (IStrategy strategy in _strategies)
        {
            IEnumerable<Source> sources = strategy.Generate(match.Subject);

            foreach (Source source in sources)
            {
                string code = Wrap(source.Code, match.Subject, match.SupportsNullableReferenceTypes);
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

    private static Subject? Parse(TypeDeclarationSyntax? syntax, Compilation compilation, CancellationToken cancellationToken)
    {
        return syntax.ToSubject(compilation, cancellationToken);
    }

    private static TypeDeclarationSyntax? Transform(GeneratorSyntaxContext context, CancellationToken cancellationToken)
    {
        return context.Node as TypeDeclarationSyntax;
    }

    private static string Nest(string code, Subject subject)
    {
        foreach (Nesting parent in subject.Nesting.Reverse())
        {
            code = code.Indent();

            code = $$"""
                {{parent.Declaration}} {{parent.Qualification}}
                {
                    {{code}}
                }
                """;
        }

        return code;
    }

    private static string Wrap(string code, Subject subject, bool supportsNullableReferenceTypes)
    {
        code = Nest(code, subject);

        if (supportsNullableReferenceTypes)
        {
            code = $"""
                #nullable disable
                #pragma warning disable CS8625

                {code}
                
                #pragma warning restore CS8625
                #nullable restore
                """;
        }

        code = $"""
            using System;
            using System.Collections.Generic;

            {code}
            """;

        if (subject.IsGlobal)
        {
            return code;
        }

        return $$"""
            namespace {{subject.Namespace}}
            {
                {{code.Indent()}}
            }
            """;
    }
}