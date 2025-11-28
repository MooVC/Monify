namespace Monify.Strategies;

using Monify.Model;
using static Monify.Model.Subject;

/// <summary>
/// Generates operators to forward binary operators supported by the encapsulated type.
/// </summary>
internal sealed class BinaryOperatorStrategy
    : IStrategy
{
    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        for (int index = 0; index < subject.Encapsulated.Length; index++)
        {
            Encapsulated encapsulated = subject.Encapsulated[index];

            if (encapsulated.BinaryOperators.IsDefaultOrEmpty)
            {
                continue;
            }

            string hintPrefix = index == IndexForEncapsulatedValue
                ? "BinaryOperators"
                : $"BinaryOperators.Passthrough.Level{index:D2}";

            for (int binaryIndex = 0; binaryIndex < encapsulated.BinaryOperators.Length; binaryIndex++)
            {
                BinaryOperator binary = encapsulated.BinaryOperators[binaryIndex];

                string hint = $"{hintPrefix}.{binaryIndex:D2}";
                string code = CreateOperator(subject, binary);

                yield return new Source(code, hint);
            }
        }
    }

    private static string CreateOperator(Subject subject, BinaryOperator binary)
    {
        string leftType = binary.IsLeftSubject ? subject.Qualification : binary.Left;
        string rightType = binary.IsRightSubject ? subject.Qualification : binary.Right;
        string returnType = binary.IsReturnSubject ? subject.Qualification : binary.Return;

        string leftOperand = binary.IsLeftSubject ? "left._value" : "left";
        string rightOperand = binary.IsRightSubject ? "right._value" : "right";
        string operation = $"{leftOperand} {binary.Symbol} {rightOperand}";

        string result = binary.IsReturnSubject
            ? $"return new {subject.Qualification}({operation});"
            : $"return ({returnType})({operation});";

        string guard = string.Concat(
            CreateGuard(binary.IsLeftSubject, "left"),
            CreateGuard(binary.IsRightSubject, "right"));

        string body = guard.Length > 0
            ? $"{guard}        {result}"
            : $"        {result}";

        if (guard.Length > 0)
        {
            return $$"""
                {{subject.Declaration}} {{subject.Qualification}}
                {
                    public static {{returnType}} operator {{binary.Symbol}}({{leftType}} left, {{rightType}} right)
                    {
                        {{body}}
                    }
                }
                """;
        }

        return $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                public static {{returnType}} operator {{binary.Symbol}}({{leftType}} left, {{rightType}} right)
                {
                    {{body}}
                }
            }
            """;
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