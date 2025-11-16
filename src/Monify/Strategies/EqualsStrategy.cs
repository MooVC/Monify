namespace Monify.Strategies;

using Monify.Model;

/// <summary>
/// Generates the source needed to support <see cref="object.Equals(object)"/>.
/// </summary>
internal sealed class EqualsStrategy
    : IStrategy
{
    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        if (!subject.CanOverrideEquals)
        {
            yield break;
        }

        string code = $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                public override bool Equals(object other)
                {
                    if (other is {{subject.Qualification}})
                    {
                        return Equals(({{subject.Qualification}})other);
                    }

                    return false;
                }
            }
            """;

        yield return new Source(code, nameof(Equals));
    }
}