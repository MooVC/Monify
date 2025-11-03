namespace Monify;

using System;
using Microsoft.CodeAnalysis;
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
        new ConstructorStrategy(),
        new ConvertFromStrategy(),
        new ConvertToStrategy(),
        new EqualityStrategy(subject => !subject.HasEqualityOperatorForSelf, "Self", subject => subject.Qualification),
        new EqualityStrategy(subject => !subject.HasEqualityOperatorForValue, "Value", subject => subject.Value),
        new EqualsStrategy(),
        new EquatableStrategy(
            subject => !subject.IsEquatableToSelf,
            _ => $"Equals(other.{FieldStrategy.Name})",
            subject => !subject.HasEquatableForSelf,
            "Self",
            subject => subject.Qualification),
        new EquatableStrategy(
            subject => !subject.IsEquatableToValue,
            GetEqualityOperator,
            subject => !subject.HasEquatableForValue,
            "Value",
            subject => subject.Value),
        new FieldStrategy(),
        new GetHashCodeStrategy(),
        new InequalityStrategy(subject => !subject.HasInequalityOperatorForSelf, "Self", subject => subject.Qualification),
        new InequalityStrategy(subject => !subject.HasInequalityOperatorForValue, "Value", subject => subject.Value),
        new ToStringStrategy(),
    };

    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<TypeDeclarationSyntax?> classes = context
            .SyntaxProvider
            .CreateSyntaxProvider(predicate: IsMatch, transform: Transform)
            .Where(record => record is not null);

        IncrementalValuesProvider<Subject?> subjects = classes
           .Combine(context.CompilationProvider)
           .Select(static (match, cancellationToken) => Parse(match.Left, match.Right, cancellationToken))
           .Where(subject => subject is not null);

        context.RegisterSourceOutput(subjects, Generate);
    }

    private static void Generate(SourceProductionContext context, Subject? subject)
    {
        if (subject is not null)
        {
#if DEBUG
            Dictionary<string, string> files = new();
#endif

            foreach (IStrategy strategy in _strategies)
            {
                IEnumerable<Source> sources = strategy.Generate(subject);

                foreach (Source source in sources)
                {
                    string code = Wrap(source.Code, subject);
                    string hint = GetHint(source, subject);

#if DEBUG
                    files[hint] = code;
#endif

                    context.AddSource(hint, code);
                }
            }
        }
    }

    private static string GetEqualityOperator(Subject subject)
    {
        if (IsImmutableArray(subject.Value))
        {
            return $$"""
                {{FieldStrategy.Name}}.IsDefault
                    ? other.IsDefault
                    : !other.IsDefault && global::Monify.Internal.SequenceEqualityComparer.Default.Equals({{FieldStrategy.Name}}, other)
                """;
        }

        if (subject.IsSequence)
        {
            return $"global::Monify.Internal.SequenceEqualityComparer.Default.Equals({FieldStrategy.Name}, other)";
        }

        return $"global::System.Collections.Generic.EqualityComparer<{subject.Value}>.Default.Equals({FieldStrategy.Name}, other)";
    }

    private static bool IsImmutableArray(string value)
    {
        return value.StartsWith("global::System.Collections.Immutable.ImmutableArray<", StringComparison.Ordinal);
    }

    private static string GetHint(Source source, Subject subject)
    {
        string name = subject.Name;

        if (subject.Nesting.Length > 0)
        {
            IEnumerable<string> names = subject.Nesting
                .Reverse()
                .Select(parent => parent.Name)
                .Union(new[] { name });

            name = string.Join(".", names);
        }

        string separator = source.Hint.StartsWith(".")
            ? string.Empty
            : ".";

        return $"{subject.Namespace}.{name}{separator}{source.Hint}.g.cs";
    }

    private static bool IsMatch(SyntaxNode node, CancellationToken cancellationToken)
    {
        return node is TypeDeclarationSyntax type && type.AttributeLists.Count > 0;
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

            #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            #nullable disable
            #endif

            {code}

            #if NET5_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            #nullable restore
            #endif
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