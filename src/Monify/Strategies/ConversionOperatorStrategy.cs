namespace Monify.Strategies;

using Monify.Model;
using static Monify.Model.Subject;

/// <summary>
/// Generates operators to forward conversions supported by the encapsulated type.
/// </summary>
internal sealed class ConversionOperatorStrategy
    : IStrategy
{
    private const string ExplicitOperatorName = "op_Explicit";

    /// <inheritdoc/>
    public IEnumerable<Source> Generate(Subject subject)
    {
        for (int index = 0; index < subject.Encapsulated.Length; index++)
        {
            Encapsulated encapsulated = subject.Encapsulated[index];

            if (encapsulated.Conversions.IsDefaultOrEmpty)
            {
                continue;
            }

            string hintPrefix = index == IndexForEncapsulatedValue
                ? "ConversionOperators"
                : $"ConversionOperators.Passthrough.Level{index:D2}";

            for (int conversionIndex = 0; conversionIndex < encapsulated.Conversions.Length; conversionIndex++)
            {
                Conversion conversion = encapsulated.Conversions[conversionIndex];

                string hint = $"{hintPrefix}.{conversionIndex:D2}";
                string code = CreateConversion(subject, encapsulated, conversion);

                yield return new Source(code, hint);
            }
        }
    }

    private static string CreateConversion(Subject subject, Encapsulated encapsulated, Conversion conversion)
    {
        string operatorName = conversion.Operator == ExplicitOperatorName
            ? "explicit"
            : "implicit";

        string parameter = conversion.IsParameterSubject
            ? subject.Qualification
            : conversion.Parameter;

        string result = conversion.IsReturnSubject
            ? subject.Qualification
            : conversion.Return;

        if (conversion.IsParameterSubject)
        {
            return $$"""
                {{subject.Declaration}} {{subject.Qualification}}
                {
                    public static {{operatorName}} operator {{result}}({{parameter}} subject)
                    {
                        if (ReferenceEquals(subject, null))
                        {
                            throw new ArgumentNullException("subject");
                        }

                        return ({{result}})subject._value;
                    }
                }
                """;
        }

        return $$"""
            {{subject.Declaration}} {{subject.Qualification}}
            {
                public static {{operatorName}} operator {{result}}({{parameter}} value)
                {
                    return new {{subject.Qualification}}(({{encapsulated.Type}})value);
                }
            }
            """;
    }
}