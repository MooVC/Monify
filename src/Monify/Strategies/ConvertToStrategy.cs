namespace Monify.Strategies;

using Monify.Model;

/// <summary>
/// Generates the source needed to allow for the encapsulated type to be implicitly cast from the encapsulating type.
/// </summary>
internal sealed class ConvertToStrategy
    : IStrategy
{
    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        if (subject.HasConversionTo)
        {
            yield break;
        }

        string code = $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                public static implicit operator {{subject.Qualification}}({{subject.Value}} value)
                {
                    return new {{subject.Qualification}}(value);
                }
            }
            """;

        yield return new Source(code, "ConvertTo");
    }
}