namespace Monify.Strategies
{
    using System;
    using System.Collections.Generic;
    using Monify.Model;
    using static Monify.Model.Subject;
    using static Monify.Strategies.UnaryOperatorStrategy_Resources;

    /// <summary>
    /// Generates operators to forward unary operators supported by the encapsulated type.
    /// </summary>
    internal sealed class UnaryOperatorStrategy
        : IStrategy
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Generate(Subject subject)
        {
            var signatures = new HashSet<(string Operator, string Parameter)>();

            for (int index = 0; index < subject.Encapsulated.Length; index++)
            {
                Encapsulated encapsulated = subject.Encapsulated[index];

                if (encapsulated.UnaryOperators.IsDefaultOrEmpty)
                {
                    continue;
                }

                string hintPrefix = index == IndexForEncapsulatedValue
                    ? "Unary"
                    : $"Unary.Passthrough.Level{index:D2}";

                foreach (UnaryOperator unary in encapsulated.UnaryOperators)
                {
                    string parameterType = subject.Qualification;

                    if (!signatures.Add((unary.Operator, parameterType)))
                    {
                        continue;
                    }

                    string subjectHint = parameterType.NormalizeTypeForHint();
                    string hint = $"{hintPrefix}.{unary.Operator}.{subjectHint}";
                    string code = CreateOperator(subject, encapsulated, unary);

                    yield return new Source(code, hint);
                }
            }
        }

        private static string CreateOperator(Subject subject, Encapsulated encapsulated, UnaryOperator unary)
        {
            (string operand, bool requiresValueCopy) = GetOperand(unary.Symbol);
            string operation = ApplyOperator(unary.Symbol, operand);

            string returnType = unary.IsReturnSubject
                ? subject.Qualification
                : unary.Return;

            string result = unary.IsReturnSubject
                ? string.Format(SubjectResultSource, subject.Qualification, operation)
                : string.Format(ValueResultSource, returnType, operation);

            if (requiresValueCopy)
            {
                return string.Format(
                    ValueCopyOperatorSource,
                    subject.Declaration,
                    subject.Qualification,
                    returnType,
                    unary.Symbol,
                    subject.Qualification,
                    encapsulated.Type,
                    result);
            }

            return string.Format(OperatorSource, subject.Declaration, subject.Qualification, returnType, unary.Symbol, subject.Qualification, result);
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