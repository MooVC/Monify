namespace Monify.Strategies
{
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Monify.Model;
    using static Monify.Model.Subject;

    using static Monify.Strategies.BinaryOperatorStrategy_Resources;

    /// <summary>
    /// Generates operators to forward binary operators supported by the encapsulated type.
    /// </summary>
    internal sealed class BinaryOperatorStrategy
        : IStrategy
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Generate(Subject subject)
        {
            var signatures = new HashSet<(string Operator, string Left, string Right)>();

            for (int index = 0; index < subject.Encapsulated.Length; index++)
            {
                Encapsulated encapsulated = subject.Encapsulated[index];

                if (encapsulated.BinaryOperators.IsDefaultOrEmpty)
                {
                    continue;
                }

                string hintPrefix = index == IndexForEncapsulatedValue
                    ? "Binary"
                    : $"Binary.Passthrough.Level{index:D2}";

                foreach (BinaryOperator binary in encapsulated.BinaryOperators)
                {
                    string leftType = binary.IsLeftSubject ? subject.Qualification : binary.Left;
                    string rightType = binary.IsRightSubject ? subject.Qualification : binary.Right;

                    if (!signatures.Add((binary.Operator, leftType, rightType)))
                    {
                        continue;
                    }

                    string leftHint = leftType.NormalizeTypeForHint();
                    string rightHint = rightType.NormalizeTypeForHint();
                    string hint = $"{hintPrefix}.{binary.Operator}.{leftHint}-{rightHint}";
                    string code = CreateOperator(subject, binary);

                    yield return new Source(code, hint);
                }
            }
        }

        private static string CreateOperator(Subject subject, BinaryOperator binary)
        {
            string leftType = binary.IsLeftSubject ? subject.Qualification : binary.Left;
            string rightType = binary.IsRightSubject ? subject.Qualification : binary.Right;
            string returnType = binary.IsReturnSubject ? subject.Qualification : binary.Return;

            string leftOperand = binary.IsLeftSubject ? "left._value" : "left";
            string rightOperand = binary.IsRightSubject ? "right._value" : "right";
            string operation = string.Format(OperationSource, leftOperand, binary.Symbol, rightOperand);

            string result = binary.IsReturnSubject
                ? string.Format(SubjectResultSource, subject.Qualification, operation)
                : string.Format(ValueResultSource, returnType, operation);

            string leftGuard = CreateGuard(binary.IsLeftSubject, "left");
            string rightGuard = CreateGuard(binary.IsRightSubject, "right");

            SyntaxTrivia guardSeparator = leftGuard.Length > 0 && rightGuard.Length > 0
                ? SyntaxFactory.ElasticLineFeed
                : SyntaxFactory.ElasticMarker;

            string guard = string.Format(CombinedSource, leftGuard, guardSeparator, rightGuard);

            SyntaxTrivia separator = guard.Length > 0
                ? SyntaxFactory.ElasticLineFeed
                : SyntaxFactory.ElasticMarker;

            string body = guard.Length > 0
                ? string.Format(GuardedBodySource, guard, separator, result)
                : string.Format(BodySource, result);

            if (guard.Length > 0)
            {
                return string.Format(GuardedOperatorSource, subject.Declaration, subject.Qualification, returnType, binary.Symbol, leftType, rightType, body);
            }

            return string.Format(OperatorSource, subject.Declaration, subject.Qualification, returnType, binary.Symbol, leftType, rightType, body);
        }

        private static string CreateGuard(bool shouldGuard, string parameter)
        {
            if (!shouldGuard)
            {
                return string.Empty;
            }

            return string.Format(GuardSource, parameter, parameter);
        }
    }
}