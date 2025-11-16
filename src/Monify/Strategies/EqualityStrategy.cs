namespace Monify.Strategies;

using System.Collections.Generic;
using Monify.Model;

/// <summary>
/// Generates the source needed to support the equality operator.
/// </summary>
internal sealed partial class EqualityStrategy
    : IStrategy
{
    private const int IndexForEncapsulatedValue = 0;

    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        foreach (Operation operation in GetOperations(subject))
        {
            if (operation.HasOperator)
            {
                continue;
            }

            string code = CreateEquality(subject, operation.Type);

            yield return new Source(code, operation.Hint);
        }
    }

    private static IEnumerable<Operation> GetOperations(Subject subject)
    {
        yield return new Operation(subject.HasEqualityOperator, "Equality.Self", false, subject.Qualification);

        for (int index = 0; index < subject.Encapsulated.Length; index++)
        {
            Encapsulated conversion = subject.Encapsulated[index];

            string hint = index == IndexForEncapsulatedValue
                ? "Equality.Value"
                : $"Equality.Passthrough.Level{index:D2}";

            yield return new Operation(conversion.HasEqualityOperator, hint, true, conversion.Type);
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
}