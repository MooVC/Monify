namespace Monify.Strategies;

using System.Collections.Generic;
using Monify.Model;

/// <summary>
/// Generates the source needed to support the inequality operator.
/// </summary>
internal sealed class InequalityStrategy
    : IStrategy
{
    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        foreach (InequalityOperation operation in GetOperations(subject))
        {
            if (operation.HasOperator)
            {
                continue;
            }

            yield return new Source(CreateInequality(subject, operation.Type), operation.Hint);
        }
    }

    private static IEnumerable<InequalityOperation> GetOperations(Subject subject)
    {
        yield return new InequalityOperation(subject.HasInequalityOperatorForSelf, subject.Qualification, "Inequality.Self");
        yield return new InequalityOperation(subject.HasInequalityOperatorForValue, subject.Value, "Inequality.Value");

        for (int index = 1; index < subject.Operators.Length; index++)
        {
            Operators conversion = subject.Operators[index];

            yield return new InequalityOperation(
                conversion.HasInequalityOperator,
                conversion.Type,
                GetPassthroughHint(index));
        }
    }

    private static string CreateInequality(Subject subject, string type)
    {
        return $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                public static bool operator !=({{subject.Qualification}} left, {{type}} right)
                {
                    return !(left == right);
                }
            }
            """;
    }

    private static string GetPassthroughHint(int index)
    {
        if (index == 1)
        {
            return "Inequality.Passthrough";
        }

        return $"Inequality.Passthrough.Level{index:D2}";
    }

    private readonly record struct InequalityOperation(bool HasOperator, string Type, string Hint);
}
