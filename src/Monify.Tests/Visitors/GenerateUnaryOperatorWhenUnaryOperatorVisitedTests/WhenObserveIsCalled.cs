namespace Monify.Visitors.GenerateUnaryOperatorWhenUnaryOperatorVisitedTests;

using System.Linq;
using Monify.Model;
using Monify.Visitors;
using UnaryOperatorsNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.UnaryOperators;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenPassthroughUnaryOperatorThenHintIncludesEncapsulatedIndex()
    {
        // Arrange
        const string booleanType = "bool";

        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues =
        [
            new Encapsulated { Type = "global::Sample.Inner" },
            new Encapsulated
            {
                Type = booleanType,
                UnaryOperators =
                [
                    new UnaryOperator
                    {
                        IsReturnSubject = false,
                        Operator = "op_LogicalNot",
                        Return = booleanType,
                        Symbol = "!",
                    },
                ],
            },
        ];
        var visitor = new GenerateUnaryOperatorWhenUnaryOperatorVisited();

        // Act
        Source source = visitor.Observe(OperatorAt(subject, encapsulatedIndex: 1)).Single();

        // Assert
        source.Hint.ShouldBe("Unary.Passthrough.Level01.op_LogicalNot.Sample");
    }

    [Test]
    public void GivenUnaryOperatorsThenSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues =
        [
            new Encapsulated
            {
                Type = "int",
                UnaryOperators =
                [
                    new UnaryOperator
                    {
                        IsReturnSubject = true,
                        Operator = "op_UnaryNegation",
                        Return = subject.Qualification,
                        Symbol = "-",
                    },
                    new UnaryOperator
                    {
                        IsReturnSubject = true,
                        Operator = "op_Increment",
                        Return = subject.Qualification,
                        Symbol = "++",
                    },
                    new UnaryOperator
                    {
                        IsReturnSubject = false,
                        Operator = "op_True",
                        Return = "bool",
                        Symbol = "true",
                    },
                ],
            },
        ];
        var visitor = new GenerateUnaryOperatorWhenUnaryOperatorVisited();

        // Act
        Source negation = visitor.Observe(OperatorAt(subject, operatorIndex: 0)).Single();
        Source increment = visitor.Observe(OperatorAt(subject, operatorIndex: 1)).Single();
        Source truth = visitor.Observe(OperatorAt(subject, operatorIndex: 2)).Single();

        // Assert
        negation.Hint.ShouldBe("Unary.op_UnaryNegation.Sample");
        negation.Code.ShouldContain("operator -");
        negation.Code.ShouldContain("new Sample(-subject._value)");
        increment.Code.ShouldContain("value = subject._value;");
        increment.Code.ShouldContain("new Sample(++value)");
        truth.Code.ShouldContain("operator true");
        truth.Code.ShouldContain("return (bool)subject._value ? true : false;");
    }

    private static UnaryOperatorsNode.UnaryOperator OperatorAt(Subject subject, int encapsulatedIndex = 0, int operatorIndex = 0)
    {
        var encapsulated = TestSubject.EncapsulatedAt(subject, encapsulatedIndex);
        var operators = new UnaryOperatorsNode(encapsulated, subject, encapsulated._Value.UnaryOperators);

        return new UnaryOperatorsNode.UnaryOperator(operators, operatorIndex, subject, operators._Value[operatorIndex]);
    }
}