namespace Monify.Strategies;

using System.Collections.Generic;
using Monify.Model;

/// <summary>
/// Generates the source needed to support the equality operator.
/// </summary>
internal sealed class EqualityStrategy
    : IStrategy
{
    private readonly Predicate<Subject> _condition;
    private readonly string _name;
    private readonly Func<Subject, string> _type;

    /// <summary>
    /// Creates a new instance of the <see cref="EqualityStrategy"/>.
    /// </summary>
    /// <param name="condition">
    /// The condition for which, when <see langword="true" />, will result in code generation.
    /// </param>
    /// <param name="name">
    /// The name of the equality operator, used as part of the hint name for the generated code.
    /// </param>
    /// <param name="type">
    /// The qualification for the type which serves as the subject for comparison.
    /// </param>
    public EqualityStrategy(Predicate<Subject> condition, string name, Func<Subject, string> type)
    {
        _condition = condition;
        _name = name;
        _type = type;
    }

    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        if (!_condition(subject))
        {
            yield break;
        }

        string type = _type(subject);

        string code = $$"""
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

        yield return new Source(code, $"Equality.{_name}");
    }
}
