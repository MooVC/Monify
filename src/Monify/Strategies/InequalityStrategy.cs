namespace Monify.Strategies;

using System.Collections.Generic;
using Monify.Model;

/// <summary>
/// Generates the source needed to support the inequality operator.
/// </summary>
internal sealed partial class InequalityStrategy
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

            yield return new Source(CreateInequality(subject, operation.Type), operation.Hint);
        }
    }

    private static IEnumerable<Operation> GetOperations(Subject subject)
    {
        yield return new Operation(subject.HasInequalityOperator, "Inequality.Self", subject.Qualification);

        for (int index = 0; index < subject.Encapsulated.Length; index++)
        {
            Encapsulated conversion = subject.Encapsulated[index];

            string hint = index == IndexForEncapsulatedValue
                ? "Inequality.Value"
                : $"Inequality.Passthrough.Level{index:D2}";

            yield return new Operation(conversion.HasInequalityOperator, hint, conversion.Type);
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
}