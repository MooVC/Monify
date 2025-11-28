namespace Monify.Strategies;

using System;
using Monify.Model;
using static Monify.Model.Subject;

/// <summary>
/// Generates operators to forward unary operators supported by the encapsulated type.
/// </summary>
internal sealed class UnaryOperatorStrategy
    : IStrategy
{
    private const string Indentation = "            ";

    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        for (int index = 0; index < subject.Encapsulated.Length; index++)
        {
            Encapsulated encapsulated = subject.Encapsulated[index];

            if (encapsulated.UnaryOperators.IsDefaultOrEmpty)
            {
                continue;
            }

            string hintPrefix = index == IndexForEncapsulatedValue
                ? "UnaryOperators"
                : $"UnaryOperators.Passthrough.Level{index:D2}";

            for (int unaryIndex = 0; unaryIndex < encapsulated.UnaryOperators.Length; unaryIndex++)
            {
                UnaryOperator unary = encapsulated.UnaryOperators[unaryIndex];

                string hint = $"{hintPrefix}.{unaryIndex:D2}";
                string code = CreateOperator(subject, encapsulated, unary);

                yield return new Source(code, hint);
            }
        }
    }

    private static string CreateOperator(Subject subject, Encapsulated encapsulated, UnaryOperator unary)
    {
        (string operand, bool requiresValueCopy) = GetOperand(unary.Symbol);
        string operation = ApplyOperator(unary.Symbol, operand);
        string returnType = unary.IsReturnSubject ? subject.Qualification : unary.Return;
        string valueDeclaration = requiresValueCopy
            ? $"{encapsulated.Type} value = subject._value;{Environment.NewLine}{Environment.NewLine}{Indentation}"
            : string.Empty;
        string result = unary.IsReturnSubject
            ? $"return new {subject.Qualification}({operation});"
            : $"return ({returnType}){operation};";

        return $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                public static {{returnType}} operator {{unary.Symbol}}({{subject.Qualification}} subject)
                {
                    if (ReferenceEquals(subject, null))
                    {
                        throw new ArgumentNullException("subject");
                    }

                    {{valueDeclaration}}{{result}}
                }
            }
            """;
    }

    private static string ApplyOperator(string symbol, string operand)
    {
        return symbol switch
        {
            "-" => $"-{operand}",
            "!" => $"!{operand}",
            "++" => $"++{operand}",
            "--" => $"--{operand}",
            "~" => $"~{operand}",
            "+" => $"+{operand}",
            "true" => $"{operand} ? true : false",
            "false" => $"{operand} ? false : true",
            _ => throw new NotSupportedException($"Unsupported unary operator: {symbol}"),
        };
    }

    private static (string Operand, bool RequiresValueCopy) GetOperand(string symbol)
    {
        bool requiresValueCopy = symbol is "++" or "--";
        string operand = requiresValueCopy ? "value" : "subject._value";

        return (operand, requiresValueCopy);
    }
}
