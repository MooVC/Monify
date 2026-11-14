namespace Monify.Visitors
{
    using System.Collections.Generic;
    using Monify.Model;
    using Monify.Syntax;
    using MooVC.Syntax;
    using MooVC.Syntax.CSharp;
    using static Monify.Model.Subject;
    using static Monify.Visitors.GenerateBinaryOperatorWhenBinaryOperatorVisited_Resources;
    using BinaryOperatorNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.BinaryOperators.BinaryOperator;

    /// <summary>
    /// Generates a binary operator that forwards to the encapsulated type.
    /// </summary>
    internal sealed class GenerateBinaryOperatorWhenBinaryOperatorVisited
        : ISubjectVisitor<BinaryOperatorNode, Source>
    {
        /// <inheritdoc/>
        public IEnumerable<Source> Observe(BinaryOperatorNode instance)
        {
            BinaryOperator binary = instance._Value;
            Subject subject = instance._Root;
            int index = instance.BinaryOperators.Encapsulated._Index;
            string hintPrefix = index == IndexForEncapsulatedValue
                ? "Binary"
                : $"Binary.Passthrough.Level{index:D2}";

            string leftType = binary.IsLeftSubject ? subject.Qualification : binary.Left;
            string rightType = binary.IsRightSubject ? subject.Qualification : binary.Right;
            string leftHint = leftType.NormalizeTypeForHint();
            string rightHint = rightType.NormalizeTypeForHint();
            string hint = $"{hintPrefix}.{binary.Operator}.{leftHint}-{rightHint}";

            yield return subject.ToSource(hint, CreateOperator(subject, binary));
        }

        private static Snippet CreateOperator(Subject subject, BinaryOperator binary)
        {
            string self = subject.GetSelfType();
            string leftType = binary.IsLeftSubject ? self : binary.Left;
            string rightType = binary.IsRightSubject ? self : binary.Right;
            string returnType = binary.IsReturnSubject ? self : binary.Return;

            string leftOperand = binary.IsLeftSubject ? "left._value" : "left";
            string rightOperand = binary.IsRightSubject ? "right._value" : "right";
            string operation = string.Format(OperationSource, leftOperand, binary.Symbol, rightOperand);

            string result = binary.IsReturnSubject
                ? string.Format(SubjectResultSource, self, operation)
                : string.Format(ValueResultSource, returnType, operation);

            Snippet body = Snippet.Blank.Combine(
                CreateGuard(binary.IsLeftSubject, "left"),
                CreateGuard(binary.IsRightSubject, "right"),
                Snippet.From(result));

            return Binary.Undefined
                .WithOperator(binary.Symbol)
                .WithLeft(parameter => parameter.Named("Left").OfType(TypeReferences.FromRoslyn(leftType)))
                .WithRight(parameter => parameter.Named("Right").OfType(TypeReferences.FromRoslyn(rightType)))
                .WithResult(TypeReferences.FromRoslyn(returnType))
                .WithBody(body)
                .ToSnippet(Configuration.Options);
        }

        private static Snippet CreateGuard(bool shouldGuard, string parameter)
        {
            return shouldGuard
                ? Snippet.From(Configuration.Options, string.Format(GuardSource, parameter))
                : Snippet.Empty;
        }
    }
}