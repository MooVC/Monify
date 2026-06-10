namespace Monify.Strategies
{
    using System.Collections.Generic;
    using Monify.Model;
    using static Monify.Model.Subject;

    using static Monify.Strategies.ConvertToStrategy_Resources;

    /// <summary>
    /// Generates the source needed to allow for the encapsulated type to be implicitly cast from the encapsulating type.
    /// </summary>
    internal sealed class ConvertToStrategy
        : IStrategy
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Generate(Subject subject)
        {
            for (int index = 0; index < subject.Encapsulated.Length; index++)
            {
                Encapsulated conversion = subject.Encapsulated[index];

                if (conversion.HasConversionTo)
                {
                    continue;
                }

                string hint = index == IndexForEncapsulatedValue
                    ? "ConvertTo"
                    : $"ConvertTo.Passthrough.Level{index:D2}";

                yield return new Source(CreateConversion(subject, conversion.Type), hint);
            }
        }

        private static string CreateConversion(Subject subject, string value)
        {
            return string.Format(ConversionSource, subject.Declaration, subject.Qualification, subject.Qualification, value, subject.Qualification);
        }
    }
}