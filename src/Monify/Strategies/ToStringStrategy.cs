namespace Monify.Strategies;

using Monify.Model;

/// <summary>
/// Generates the source needed to support <see cref="object.ToString()"/>.
/// </summary>
internal sealed class ToStringStrategy
    : IStrategy
{
    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        if (!subject.CanOverrideToString)
        {
            yield break;
        }

        string code = $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                public override string ToString()
                {
                    return string.Format("{{subject.Name}} { {0} }", {{FieldStrategy.Name}});
                }
            }
            """;

        yield return new Source(code, nameof(ToString));
    }
}