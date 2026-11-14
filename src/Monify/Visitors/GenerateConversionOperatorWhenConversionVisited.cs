namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;
    using static Monify.Model.Subject;
    using static Monify.Visitors.GenerateConversionOperatorWhenConversionVisited_Resources;
    using Conversion = Monify.Model.Conversion;
    using ConversionNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Conversions.Conversion;
    using ConversionSyntax = MooVC.Syntax.CSharp.Conversion;

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

            yield return CreateConversion(subject, encapsulated._Value, conversion).ToSource(subject, hint);
        }

        private static ConversionSyntax CreateConversion(Subject subject, Encapsulated encapsulated, Conversion conversion)
        {
            string self = subject.GetSelfType();
            string result = conversion.IsReturnSubject ? self : conversion.Return;
            string body = conversion.IsParameterSubject
                ? string.Format(SubjectConversionSource, result)
                : string.Format(ValueConversionSource, self, encapsulated.Type);

            return ConversionSyntax.Undefined
                .WithMode(conversion.Operator == ExplicitOperatorName ? ConversionSyntax.Types.Explicit : ConversionSyntax.Types.Implicit)
                .WithDirection(conversion.IsParameterSubject ? ConversionSyntax.Intents.To : ConversionSyntax.Intents.From)
                .WithInput(parameter => parameter.Named(conversion.IsParameterSubject ? "Subject" : "Value"))
                .ForType(TypeReferences.FromRoslyn(conversion.IsParameterSubject ? result : conversion.Parameter))
                .WithBody(Snippet.From(Configuration.Options, body));
        }
    }
}