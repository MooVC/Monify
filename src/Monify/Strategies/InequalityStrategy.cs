namespace Monify.Strategies;

using Monify.Model;

/// <summary>
/// Generates the source needed to support the inequality operator.
/// </summary>
internal sealed class InequalityStrategy
    : IStrategy
{
    private readonly Predicate<Subject> _condition;
    private readonly string _name;
    private readonly Func<Subject, string> _type;

    /// <summary>
    /// Creates a new instance of the <see cref="InequalityStrategy"/>.
    /// </summary>
    /// <param name="condition">The condition for which, when <see langword="true" />, will result in code generation.</param>
    /// <param name="name">The name of the inequality operator, used as part of the hint name for the generated code.</param>
    /// <param name="type">The qualification for the type which serves as the subject for comparison.</param>
    public InequalityStrategy(Predicate<Subject> condition, string name, Func<Subject, string> type)
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
                public static bool operator !=({{subject.Qualification}} left, {{type}} right)
                {
                    return !(left == right);
                }
            }
            """;

        yield return new Source(code, $"Inequality.{_name}");
    }
}
