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
        for (int index = 0; index < subject.Conversions.Length; index++)
        {
            Conversion conversion = subject.Conversions[index];

            if (conversion.HasConversionTo)
            {
                continue;
            }

            string hint = index == 0
                ? "ConvertTo"
                : $"ConvertTo.Nested.{index - 1}";

            yield return new Source(CreateConversion(subject, conversion.Type), hint);
        }
    }

    private static string CreateConversion(Subject subject, string value)
    {
        return $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                public static implicit operator {{subject.Qualification}}({{value}} value)
                {
                    return new {{subject.Qualification}}(value);
                }
            }
            """;
    }
}