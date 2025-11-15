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
        for (int index = 0; index < subject.Conversions.Length; index++)
        {
            Conversion conversion = subject.Conversions[index];

            if (conversion.HasConversionFrom)
            {
                continue;
            }

            string hint = index == 0
                ? "ConvertFrom"
                : $"ConvertFrom.Nested.{index - 1}";

            yield return new Source(CreateConversion(subject, conversion.Type), hint);
        }
    }

    private static string CreateConversion(Subject subject, string value)
    {
        return $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                public static implicit operator {{value}}({{subject.Qualification}} subject)
                {
                    if (ReferenceEquals(subject, null))
                    {
                        throw new ArgumentNullException("subject");
                    }

                    return subject._value;
                }
            }
            """;
    }
}