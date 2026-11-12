namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;
    using static Monify.Model.Subject;
    using static Monify.Visitors.GenerateConversionOperatorWhenConversionVisited_Resources;
    using ConversionNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Conversions.Conversion;

    /// <summary>
    /// Generates a conversion operator that forwards to the encapsulated type.
    /// </summary>
    internal sealed class GenerateConversionOperatorWhenConversionVisited
        : ISubjectVisitor<ConversionNode, Source>
    {
        private const string ExplicitOperatorName = "op_Explicit";

        /// <inheritdoc/>
        public IEnumerable<Source> Observe(ConversionNode instance)
        {
            Conversion conversion = instance._Value;
            Subject subject = instance._Root;
            Subject.Graph.EncapsulatedValues.Encapsulated encapsulated = instance.Conversions.Encapsulated;
            int index = encapsulated._Index;
            string hintPrefix = index == IndexForEncapsulatedValue
                ? "Conversions"
                : $"Conversions.Passthrough.Level{index:D2}";
            string hint = $"{hintPrefix}.{instance._Index:D2}";

            yield return GenerationSyntax.CreateSource(subject, hint, CreateConversion(subject, encapsulated._Value, conversion));
        }

        private static Snippet CreateConversion(Subject subject, Encapsulated encapsulated, Conversion conversion)
        {
            string operatorName = conversion.Operator == ExplicitOperatorName
                ? "explicit"
                : "implicit";

            string self = GenerationSyntax.Self(subject);
            string parameter = conversion.IsParameterSubject ? self : conversion.Parameter;
            string result = conversion.IsReturnSubject ? self : conversion.Return;
            string parameterName = conversion.IsParameterSubject ? "subject" : "value";
            string body = conversion.IsParameterSubject
                ? string.Format(SubjectConversionSource, result)
                : string.Format(ValueConversionSource, self, encapsulated.Type);

            return GenerationSyntax.Block(
                GenerationSyntax.ToSnippet(body),
                $"public static {operatorName} operator {result}({parameter} {parameterName})");
        }
    }
}