namespace Monify.Visitors.GenerateBinaryOperatorWhenBinaryOperatorVisitedTests;

using System.Linq;
using Monify.Model;
using Monify.Visitors;
using BinaryOperatorsNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.BinaryOperators;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenBinaryOperatorsThenSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.QualifiedName = "global::Sample";
        subject.EncapsulatedValues =
        [
            new Encapsulated
            {
                Type = "int",
                BinaryOperators =
                [
                    new BinaryOperator
                    {
                        IsLeftSubject = true,
                        IsReturnSubject = true,
                        IsRightSubject = true,
                        Left = subject.Qualification,
                        Operator = "op_Addition",
                        Return = subject.Qualification,
                        Right = subject.Qualification,
                        Symbol = "+",
                    },
                    new BinaryOperator
                    {
                        IsLeftSubject = true,
                        IsReturnSubject = true,
                        IsRightSubject = false,
                        Left = subject.Qualification,
                        Operator = "op_Subtraction",
                        Return = subject.Qualification,
                        Right = "int",
                        Symbol = "-",
                    },
                    new BinaryOperator
                    {
                        IsLeftSubject = false,
                        IsReturnSubject = false,
                        IsRightSubject = true,
                        Left = "int",
                        Operator = "op_GreaterThan",
                        Return = "bool",
                        Right = subject.Qualification,
                        Symbol = ">",
                    },
                ],
            },
        ];
        var visitor = new GenerateBinaryOperatorWhenBinaryOperatorVisited();

        // Act
        Source addition = visitor.Observe(OperatorAt(subject, operatorIndex: 0)).Single();
        Source subtraction = visitor.Observe(OperatorAt(subject, operatorIndex: 1)).Single();
        Source comparison = visitor.Observe(OperatorAt(subject, operatorIndex: 2)).Single();
        string oneSidedGuardSource = subtraction.Code.Replace("\r\n", "\n");

        // Assert
        addition.Hint.ShouldBe("Binary.op_Addition.Sample-Sample");
        addition.Code.ShouldContain("operator +(global::Sample left, global::Sample right)");
        addition.Code.ShouldContain("new global::Sample(left._value + right._value)");
        subtraction.Code.ShouldContain("operator -(global::Sample left, int right)");
        subtraction.Code.ShouldContain("throw new global::System.ArgumentNullException(\"left\")");
        oneSidedGuardSource.ShouldContain("throw new global::System.ArgumentNullException(\"left\");\n        }\n\n        return new global::Sample(left._value - right);");
        comparison.Code.ShouldContain("operator >(int left, global::Sample right)");
        comparison.Code.ShouldContain("return (bool)(left > right._value);");
    }

    [Test]
    public void GivenPassthroughBinaryOperatorThenHintIncludesEncapsulatedIndex()
    {
        // Arrange
        const string objectType = "object";

        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues =
        [
            new Encapsulated { Type = "global::Sample.Inner" },
            new Encapsulated
            {
                Type = "string",
                BinaryOperators =
                [
                    new BinaryOperator
                    {
                        IsLeftSubject = true,
                        IsReturnSubject = true,
                        IsRightSubject = false,
                        Left = subject.Qualification,
                        Operator = "op_Addition",
                        Return = subject.Qualification,
                        Right = objectType,
                        Symbol = "+",
                    },
                ],
            },
        ];
        var visitor = new GenerateBinaryOperatorWhenBinaryOperatorVisited();

        // Act
        Source source = visitor.Observe(OperatorAt(subject, encapsulatedIndex: 1)).Single();

        // Assert
        source.Hint.ShouldBe("Binary.Passthrough.Level01.op_Addition.Sample-object");
    }

    private static BinaryOperatorsNode.BinaryOperator OperatorAt(Subject subject, int encapsulatedIndex = 0, int operatorIndex = 0)
    {
        var encapsulated = TestSubject.EncapsulatedAt(subject, encapsulatedIndex);
        var operators = new BinaryOperatorsNode(encapsulated, subject, encapsulated._Value.BinaryOperators);

        return new BinaryOperatorsNode.BinaryOperator(operators, operatorIndex, subject, operators._Value[operatorIndex]);
    }
}