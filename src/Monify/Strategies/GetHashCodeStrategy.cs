namespace Monify.Strategies;

using Monify.Model;

/// <summary>
/// Generates the source needed to support <see cref="object.Equals(object)"/>.
/// </summary>
internal sealed class GetHashCodeStrategy
    : IStrategy
{
    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        if (!subject.CanOverrideGetHashCode)
        {
            yield break;
        }

        string code = $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                public override int GetHashCode()
                {
                    return {{FieldStrategy.Name}}.GetHashCode();
                }
            }
            """;

        yield return new Source(code, nameof(GetHashCode));
    }
}
