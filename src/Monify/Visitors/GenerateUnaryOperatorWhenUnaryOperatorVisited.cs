namespace Monify.Visitors
{
    using System;
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;
    using static Monify.Model.Subject;
    using static Monify.Visitors.GenerateUnaryOperatorWhenUnaryOperatorVisited_Resources;
    using UnaryOperatorNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.UnaryOperators.UnaryOperator;

    /// <summary>
    /// Generates a unary operator that forwards to the encapsulated type.
    /// </summary>
    internal sealed class GenerateUnaryOperatorWhenUnaryOperatorVisited
        : ISubjectVisitor<UnaryOperatorNode, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(UnaryOperatorNode instance)
        {
            UnaryOperator unary = instance._Value;
            Subject subject = instance._Root;
            Subject.Graph.EncapsulatedValues.Encapsulated encapsulated = instance.UnaryOperators.Encapsulated;
            int index = encapsulated._Index;
            string hintPrefix = index == IndexForEncapsulatedValue
                ? "Unary"
                : $"Unary.Passthrough.Level{index:D2}";
            string subjectHint = subject.Qualification.NormalizeTypeForHint();

            string hint = $"{hintPrefix}.{unary.Operator}.{subjectHint}";

            yield return subject.ToSource(hint, CreateOperator(subject, encapsulated._Value, unary));
        }

        private static Snippet CreateOperator(Subject subject, Encapsulated encapsulated, UnaryOperator unary)
        {
            (string operand, bool requiresValueCopy) = GetOperand(unary.Symbol);
            string operation = ApplyOperator(unary.Symbol, operand);

            string self = subject.GetSelfType();
            string returnType = unary.IsReturnSubject ? self : unary.Return;

            string result = unary.IsReturnSubject
                ? string.Format(SubjectResultSource, self, operation)
                : string.Format(ValueResultSource, returnType, operation);

            string body = requiresValueCopy
                ? string.Format(ValueCopyOperatorSource, encapsulated.Type, result)
                : string.Format(OperatorSource, result);

            return Unary.Undefined
                .WithOperator(unary.Symbol)
                .WithOperand(parameter => parameter.Named("Subject").OfType(TypeReferences.FromRoslyn(self)))
                .WithResult(TypeReferences.FromRoslyn(returnType))
                .WithBody(Snippet.From(Configuration.Options, body))
                .ToSnippet(Configuration.Options);
        }

        private static string ApplyOperator(string symbol, string operand)
        {
            switch (symbol)
            {
                case "-":
                case "!":
                case "++":
                case "--":
                case "~":
                case "+":

                    return string.Format(OperationSource, symbol, operand);

                case "true":

                    return string.Format(TrueOperationSource, operand);

                case "false":

                    return string.Format(FalseOperationSource, operand);

                default:

                    throw new NotSupportedException(string.Format(ApplyOperatorNotSupported, symbol));
            }
        }

        private static (string Operand, bool RequiresValueCopy) GetOperand(string symbol)
        {
            bool requiresValueCopy = symbol == "++" || symbol == "--";
            string operand = requiresValueCopy ? "value" : "subject._value";

            return (operand, requiresValueCopy);
        }
    }
}