namespace Monify.Strategies;

using System.Collections.Generic;
using Monify.Model;

/// <summary>
/// Generates the source needed to support the equality operator.
/// </summary>
internal sealed class EqualityStrategy
    : IStrategy
{
    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        foreach (EqualityOperation operation in GetOperations(subject))
        {
            if (operation.HasOperator)
            {
                continue;
            }

            string code = operation.IsPassthrough
                ? CreatePassthroughEquality(subject, operation.Type)
                : CreateEquality(subject, operation.Type);

            yield return new Source(code, operation.Hint);
        }
    }

    private static IEnumerable<EqualityOperation> GetOperations(Subject subject)
    {
        yield return new EqualityOperation(subject.HasEqualityOperatorForSelf, subject.Qualification, "Equality.Self", false);
        yield return new EqualityOperation(subject.HasEqualityOperatorForValue, subject.Value, "Equality.Value", false);

        for (int index = 1; index < subject.Operators.Length; index++)
        {
            Operators conversion = subject.Operators[index];

            yield return new EqualityOperation(conversion.HasEqualityOperator, conversion.Type, GetPassthroughHint(index), true);
        }
    }

    private static string CreateEquality(Subject subject, string type)
    {
        return $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                public static bool operator ==({{subject.Qualification}} left, {{type}} right)
                {
                    if (ReferenceEquals(left, right))
                    {
                        return true;
                    }

                    if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                    {
                        return false;
                    }

                    return left.Equals(right);
                }
            }
            """;
    }

    private static string CreatePassthroughEquality(Subject subject, string type)
    {
        return $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                public static bool operator ==({{subject.Qualification}} left, {{type}} right)
                {
                    if (ReferenceEquals(left, right))
                    {
                        return true;
                    }

                    if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                    {
                        return false;
                    }

                    return left == ({{subject.Value}})right;
                }
            }
            """;
    }

    private static string GetPassthroughHint(int index)
    {
        if (index == 1)
        {
            return "Equality.Passthrough";
        }

        return $"Equality.Passthrough.Level{index:D2}";
    }

    private readonly record struct EqualityOperation(bool HasOperator, string Type, string Hint, bool IsPassthrough);
}
