namespace Monify.Snippets;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Monify;

internal static class SpecialTypeForwarding
{
    private const string Comparable = "global::System.IComparable";
    private const string ComparableGeneric = "global::System.IComparable<int>";

    public static Generated[] Merge(Content declaration, Generated[] expected)
    {
        if (!TryCreateTarget(declaration, out Target target))
        {
            return expected;
        }

        Generated[] interfaces =
        [
            CreateInterface(target, Comparable, "global__System_IComparable"),
            CreateInterface(target, ComparableGeneric, "global__System_IComparable_int_"),
        ];

        Generated[] methods =
        [
            CreateMethod(target, "int"),
            CreateMethod(target, "object"),
        ];

        Generated[] withInterfaces = InsertBefore(expected, interfaces, IsInequality);

        return InsertBefore(withInterfaces, methods, IsToStringOrUnary);
    }

    private static Generated CreateInterface(Target target, string @interface, string hint)
    {
        string code = $$"""
            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1710:Identifiers should have correct suffix", Justification = "Generated interface forwarding preserves the annotated type name.")]
            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1036:Override methods on comparable types", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
            [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S1210:Comparable types should implement comparison operators", Justification = "Generated interface forwarding preserves the encapsulated type contract.")]
            {{target.Declaration}} {{target.Qualification}}
                : {{@interface}}
            {
            }
            """;

        return new Generated(Wrap(code, target), Extensions.None, $"{target.Hint}.Interfaces.{hint}");
    }

    private static Generated CreateMethod(Target target, string type)
    {
        string code = $$"""
            {{target.Declaration}} {{target.Qualification}}
            {
                public int CompareTo({{type}} value)
                {
                    return _value.CompareTo(value);
                }
            }
            """;

        return new Generated(Wrap(code, target), Extensions.None, $"{target.Hint}.Methods.CompareTo.{type}");
    }

    private static string CreateTypeDeclaration(TypeDeclarationSyntax type, bool isNesting)
    {
        List<string> parts = [];

        if (type.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.ReadOnlyKeyword)))
        {
            parts.Add("readonly");
        }

        if (type.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.RefKeyword)))
        {
            parts.Add("ref");
        }

        if (type.IsClassLike() && !isNesting)
        {
            parts.Add("sealed");
        }

        parts.Add("partial");
        parts.Add(GetTypeKeyword(type));

        return string.Join(" ", parts);
    }

    private static string GetHint(BaseNamespaceDeclarationSyntax? @namespace, TypeDeclarationSyntax subject)
    {
        IEnumerable<string> parts = subject
            .Ancestors()
            .OfType<TypeDeclarationSyntax>()
            .Reverse()
            .Select(type => type.Identifier.Text)
            .Append(subject.Identifier.Text);

        string name = string.Join(".", parts);

        return @namespace is null
            ? name
            : $"{@namespace.Name}.{name}";
    }

    private static string GetQualification(TypeDeclarationSyntax type)
    {
        return $"{type.Identifier}{type.TypeParameterList}";
    }

    private static string GetTypeKeyword(TypeDeclarationSyntax type)
    {
        return type switch
        {
            ClassDeclarationSyntax => "class",
            InterfaceDeclarationSyntax => "interface",
            RecordDeclarationSyntax record when record.ClassOrStructKeyword.IsKind(SyntaxKind.StructKeyword) => "record struct",
            RecordDeclarationSyntax => "record",
            StructDeclarationSyntax => "struct",
            _ => string.Empty,
        };
    }

    private static bool IsClassLike(this TypeDeclarationSyntax type)
    {
        return type is ClassDeclarationSyntax
            || (type is RecordDeclarationSyntax record && !record.ClassOrStructKeyword.IsKind(SyntaxKind.StructKeyword));
    }

    private static bool HasMonifyForInt(TypeDeclarationSyntax type)
    {
        foreach (AttributeSyntax attribute in type.AttributeLists.SelectMany(list => list.Attributes))
        {
            string text = attribute.ToString();

            if (text.Contains("Monify", StringComparison.Ordinal)
             && (text.Contains("<int>", StringComparison.Ordinal) || text.Contains("typeof(int)", StringComparison.Ordinal)))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsInequality(Generated generated)
    {
        return generated.Hint.Contains(".Inequality.", StringComparison.Ordinal);
    }

    private static bool IsPartial(TypeDeclarationSyntax type)
    {
        return type.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.PartialKeyword));
    }

    private static bool IsToStringOrUnary(Generated generated)
    {
        return generated.Hint.EndsWith(".ToString", StringComparison.Ordinal)
            || generated.Hint.Contains(".UnaryOperators.", StringComparison.Ordinal);
    }

    private static Generated[] InsertBefore(Generated[] source, Generated[] insertions, Func<Generated, bool> predicate)
    {
        List<Generated> result = [.. source];
        int index = result.FindIndex(item => predicate(item));

        if (index < 0)
        {
            result.AddRange(insertions);

            return result.ToArray();
        }

        result.InsertRange(index, insertions);

        return result.ToArray();
    }

    private static string Nest(string code, Target target)
    {
        foreach (Nesting nesting in target.Nesting.Reverse())
        {
            code = code.Indent();

            code = $$"""
                {{nesting.Declaration}} {{nesting.Qualification}}
                {
                    {{code}}
                }
                """;
        }

        return code;
    }

    private static bool TryCreateTarget(Content declaration, out Target target)
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText(declaration.Body);
        CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
        TypeDeclarationSyntax? subject = root
            .DescendantNodes()
            .OfType<TypeDeclarationSyntax>()
            .FirstOrDefault(HasMonifyForInt);

        if (subject is null
         || !IsPartial(subject)
         || subject.Ancestors().OfType<TypeDeclarationSyntax>().Any(type => !IsPartial(type)))
        {
            target = default;

            return false;
        }

        BaseNamespaceDeclarationSyntax? @namespace = subject.Ancestors().OfType<BaseNamespaceDeclarationSyntax>().FirstOrDefault();

        target = new Target(
            CreateTypeDeclaration(subject, isNesting: false),
            GetHint(@namespace, subject),
            @namespace?.Name.ToString() ?? string.Empty,
            subject
                .Ancestors()
                .OfType<TypeDeclarationSyntax>()
                .Reverse()
                .Select(type => new Nesting(CreateTypeDeclaration(type, isNesting: true), GetQualification(type)))
                .ToArray(),
            declaration.Minimum >= LanguageVersion.CSharp8,
            GetQualification(subject));

        return true;
    }

    private static string Wrap(string code, Target target)
    {
        code = Nest(code, target);

        if (target.IsNullable)
        {
            code = $$"""
                #nullable disable
                #pragma warning disable CS8625

                {{code}}

                #pragma warning restore CS8625
                #nullable restore
                """;
        }

        code = $$"""
            using System;
            using System.Collections.Generic;

            {{code}}
            """;

        if (target.Namespace.Length == 0)
        {
            return code;
        }

        code = code.Indent();

        return $$"""
            namespace {{target.Namespace}}
            {
                {{code}}
            }
            """;
    }

    private readonly record struct Nesting(string Declaration, string Qualification);

    private readonly record struct Target(
        string Declaration,
        string Hint,
        string Namespace,
        Nesting[] Nesting,
        bool IsNullable,
        string Qualification);
}