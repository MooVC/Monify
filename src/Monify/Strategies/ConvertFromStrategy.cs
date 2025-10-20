namespace Monify.Strategies;

using Monify.Model;

/// <summary>
/// Generates the source needed to allow for the encapsulated type to be implicitly cast to the encapsulating type.
/// </summary>
internal sealed class ConvertFromStrategy
    : IStrategy
{
    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        if (subject.HasConversionFrom)
        {
            yield break;
        }

        string code = $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                public static implicit operator {{subject.Value}}({{subject.Qualification}} subject)
                {
                    if (ReferenceEquals(subject, null))
                    {
                        throw new ArgumentNullException("subject");
                    }

                    return subject._value;
                }
            }
            """;

        yield return new Source(code, "ConvertFrom");
    }
}