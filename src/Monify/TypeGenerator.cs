namespace Monify;

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
    private static readonly IStrategy[] strategies =
    [
        new ToStringStrategy(),
    ];

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValueProvider<LanguageVersion> options = context
            .ParseOptionsProvider
            .Select(GetVersion);

        IncrementalValueProvider<bool> version = options
            .Select(IsVersionSupported);

        IncrementalValuesProvider<TypeDeclarationSyntax?> classes = context
            .SyntaxProvider
            .CreateSyntaxProvider(predicate: IsMatch, transform: Transform)
            .Where(record => record is not null);

        IncrementalValuesProvider<Subject?> subjects = classes
           .Combine(context.CompilationProvider)
           .Combine(version)
           .Where(static tuple => tuple.Right)
           .Select(static (match, cancellationToken) => Parse(match.Left.Left, match.Left.Right, cancellationToken))
           .Where(subject => subject is not null);

        context.RegisterSourceOutput(subjects, Generate);
    }

    private static void Generate(SourceProductionContext context, Subject? subject)
    {
        if (subject is not null)
        {
            foreach (IStrategy strategy in strategies)
            {
                IEnumerable<Source> sources = strategy.Generate(subject);

                foreach (Source source in sources)
                {
                    string code = Wrap(source.Code, subject);
                    string hint = GetHint(source, subject);

                    context.AddSource(hint, code);
                }
            }
        }
    }

    private static string GetHint(Source source, Subject subject)
    {
        string name = subject.Name;

        if (subject.Nesting.Length > 0)
        {
            IEnumerable<string> names = subject.Nesting
                .Reverse()
                .Select(parent => parent.Name)
                .Union([name]);

            name = string.Join(".", names);
        }

        return $"{subject.Namespace}.{name}.{source.Hint}.g.cs";
    }

    private static LanguageVersion GetVersion(ParseOptions options, CancellationToken cancellationToken)
    {
        return options is CSharpParseOptions csharp
            ? csharp.LanguageVersion
            : LanguageVersion.Default;
    }

    private static bool IsMatch(SyntaxNode node, CancellationToken cancellationToken)
    {
        return node is TypeDeclarationSyntax type && type.AttributeLists.Count > 0;
    }

    private static bool IsVersionSupported(LanguageVersion version, CancellationToken cancellationToken)
    {
        return version == LanguageVersion.Default || version >= LanguageVersion.CSharp12;
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
        foreach (Nesting parent in subject.Nesting)
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

    private static string Wrap(string code, Subject subject)
    {
        code = Nest(code, subject);

        code = $"""
            using System;
            using System.Collections.Generic;

            #nullable disable
            
            {code}
            
            #nullable restore
            """;

        if (subject.IsGlobal)
        {
            return code;
        }

        return $"""
            namespace {subject.Namespace};
            
            {code}
            """;
    }
}