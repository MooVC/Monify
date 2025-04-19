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

        string code = $$$"""
            {{{subject.Qualification}}}
            {
                public override string ToString()
                {
                    return $"{{{subject.Name}}} {{ {_value} }}";
                }
            }
            """;

        yield return new Source(code, nameof(ToString));
    }
}