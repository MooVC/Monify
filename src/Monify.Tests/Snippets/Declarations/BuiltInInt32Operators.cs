namespace Monify.Snippets.Declarations;

using System.Text;

internal static class BuiltInInt32Operators
{
    private static readonly BinaryOperator[] _binaryOperators =
    [
        new("BinaryOperators.00", "Simple", "+", true),
        new("BinaryOperators.01", "Simple", "&", true),
        new("BinaryOperators.02", "Simple", "|", true),
        new("BinaryOperators.03", "Simple", "/", true),
        new("BinaryOperators.04", "Simple", "^", true),
        new("BinaryOperators.05", "bool", ">", false),
        new("BinaryOperators.06", "bool", ">=", false),
        new("BinaryOperators.07", "Simple", "<<", true, false),
        new("BinaryOperators.08", "bool", "<", false),
        new("BinaryOperators.09", "bool", "<=", false),
        new("BinaryOperators.10", "Simple", "%", true),
        new("BinaryOperators.11", "Simple", "*", true),
        new("BinaryOperators.12", "Simple", ">>", true, false),
        new("BinaryOperators.13", "Simple", "-", true),
    ];

    private static readonly UnaryOperator[] _unaryOperators =
    [
        new("UnaryOperators.00", "Simple", "--", "--value", true, true),
        new("UnaryOperators.01", "Simple", "++", "++value", true, true),
        new("UnaryOperators.02", "Simple", "~", "~subject._value", true, false),
        new("UnaryOperators.03", "Simple", "-", "-subject._value", true, false),
        new("UnaryOperators.04", "Simple", "+", "+subject._value", true, false),
    ];

    public static Generated[] CreateBinaryOperators(
        string @namespace,
        string hintPrefix,
        Nesting[] nesting,
        string declaration,
        string qualification,
        bool supportsNullableReferenceTypes)
    {
        return _binaryOperators
            .Select(@operator => @operator.ToGenerated(
                @namespace,
                hintPrefix,
                nesting,
                declaration,
                qualification,
                supportsNullableReferenceTypes))
            .ToArray();
    }

    public static Generated[] CreateUnaryOperators(
        string @namespace,
        string hintPrefix,
        Nesting[] nesting,
        string declaration,
        string qualification,
        bool supportsNullableReferenceTypes)
    {
        return _unaryOperators
            .Select(@operator => @operator.ToGenerated(
                @namespace,
                hintPrefix,
                nesting,
                declaration,
                qualification,
                supportsNullableReferenceTypes))
            .ToArray();
    }

    private static string Indent(string value)
    {
        using var reader = new StringReader(value);
        var builder = new StringBuilder();
        string? line;
        int offset = 0;

        while ((line = reader.ReadLine()) is not null)
        {
            if (++offset > 1 && !string.IsNullOrWhiteSpace(line))
            {
                _ = builder.Append("    ");
            }

            _ = builder.Append(line);
            _ = builder.AppendLine();
        }

        return builder
            .ToString()
            .TrimEnd();
    }

    private static string Nest(string code, Nesting[] nesting)
    {
        foreach (Nesting parent in nesting.Reverse())
        {
            code = Indent(code);

            code = $$"""
                {{parent.Declaration}} {{parent.Qualification}}
                {
                    {{code}}
                }
                """;
        }

        return code;
    }

    private static string Wrap(string code, string @namespace, Nesting[] nesting, bool supportsNullableReferenceTypes)
    {
        code = Nest(code, nesting);

        if (supportsNullableReferenceTypes)
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

        return $$"""
            namespace {{@namespace}}
            {
                {{Indent(code)}}
            }
            """;
    }

    public sealed record Nesting(string Declaration, string Qualification);

    private sealed record BinaryOperator(
        string Hint,
        string ReturnType,
        string Symbol,
        bool IsReturnSubject,
        bool IsRightSubject = true)
    {
        public Generated ToGenerated(
            string @namespace,
            string hintPrefix,
            Nesting[] nesting,
            string declaration,
            string qualification,
            bool supportsNullableReferenceTypes)
        {
            string rightType = IsRightSubject ? qualification : "int";
            string rightOperand = IsRightSubject ? "right._value" : "right";
            string returnType = IsReturnSubject ? qualification : ReturnType;
            string operation = $"left._value {Symbol} {rightOperand}";
            string result = IsReturnSubject
                ? $"return new {qualification}({operation});"
                : $"return ({returnType})({operation});";
            string guard = string.Concat(
                CreateGuard(shouldGuard: true, "left"),
                CreateGuard(IsRightSubject, "right"));
            string body = guard.Length > 0
                ? $"{guard}        {result}"
                : $"        {result}";

            string code = $$"""
                {{declaration}} {{qualification}}
                {
                    public static {{returnType}} operator {{Symbol}}({{qualification}} left, {{rightType}} right)
                    {
                        {{body}}
                    }
                }
                """;

            return new Generated(
                Wrap(code, @namespace, nesting, supportsNullableReferenceTypes),
                Extensions.None,
                $"{hintPrefix}.{Hint}");
        }

        private static string CreateGuard(bool shouldGuard, string parameter)
        {
            if (!shouldGuard)
            {
                return string.Empty;
            }

            return $$"""
                if (ReferenceEquals({{parameter}}, null))
                {
                    throw new ArgumentNullException("{{parameter}}");
                }

            """;
        }
    }

    private sealed record UnaryOperator(
        string Hint,
        string ReturnType,
        string Symbol,
        string Operation,
        bool IsReturnSubject,
        bool RequiresValueCopy)
    {
        public Generated ToGenerated(
            string @namespace,
            string hintPrefix,
            Nesting[] nesting,
            string declaration,
            string qualification,
            bool supportsNullableReferenceTypes)
        {
            string returnType = IsReturnSubject ? qualification : ReturnType;
            string result = IsReturnSubject
                ? $"return new {qualification}({Operation});"
                : $"return ({returnType}){Operation};";

            string code = RequiresValueCopy
                ? $$"""
                    {{declaration}} {{qualification}}
                    {
                        public static {{returnType}} operator {{Symbol}}({{qualification}} subject)
                        {
                            if (ReferenceEquals(subject, null))
                            {
                                throw new ArgumentNullException("subject");
                            }

                            int value = subject._value;

                            {{result}}
                        }
                    }
                    """
                : $$"""
                    {{declaration}} {{qualification}}
                    {
                        public static {{returnType}} operator {{Symbol}}({{qualification}} subject)
                        {
                            if (ReferenceEquals(subject, null))
                            {
                                throw new ArgumentNullException("subject");
                            }

                            {{result}}
                        }
                    }
                    """;

            return new Generated(
                Wrap(code, @namespace, nesting, supportsNullableReferenceTypes),
                Extensions.None,
                $"{hintPrefix}.{Hint}");
        }
    }
}