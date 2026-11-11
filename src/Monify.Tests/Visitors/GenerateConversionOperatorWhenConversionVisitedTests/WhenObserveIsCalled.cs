namespace Monify.Visitors.GenerateConversionOperatorWhenConversionVisitedTests;

using Monify.Model;
using Monify.Visitors;
using ConversionsNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Conversions;

public sealed class WhenObserveIsCalled
{
    [Test]
    [Arguments(0, "Conversions.01")]
    [Arguments(1, "Conversions.Passthrough.Level01.01")]
    public void GivenExplicitConversionToSubjectThenSourceIsGenerated(int index, string hint)
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var encapsulated = new Encapsulated
        {
            Conversions =
            [
                new Conversion
                {
                    IsParameterSubject = true,
                    Operator = "op_Implicit",
                    Parameter = subject.Qualification,
                    Return = "string",
                },
                new Conversion
                {
                    IsReturnSubject = true,
                    Operator = "op_Explicit",
                    Parameter = "string",
                    Return = subject.Qualification,
                },
            ],
            Type = "int",
        };
        subject.EncapsulatedValues = subject.EncapsulatedValues.Insert(index, encapsulated);
        var visitor = new GenerateConversionOperatorWhenConversionVisited();

        // Act
        Source source = visitor.Observe(ConversionAt(subject, index, conversionIndex: 1)).Single();

        // Assert
        source.Hint.ShouldBe(hint);
        source.Code.ShouldContain("explicit operator Sample(string value)");
        source.Code.ShouldContain("new Sample((int)value)");
    }

    [Test]
    public void GivenImplicitConversionFromSubjectThenSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues =
        [
            new Encapsulated
            {
                Conversions =
                [
                    new Conversion
                    {
                        IsParameterSubject = true,
                        Operator = "op_Implicit",
                        Parameter = subject.Qualification,
                        Return = "string",
                    },
                ],
                Type = "int",
            },
        ];
        var visitor = new GenerateConversionOperatorWhenConversionVisited();

        // Act
        Source source = visitor.Observe(ConversionAt(subject)).Single();

        // Assert
        source.Hint.ShouldBe("Conversions.00");
        source.Code.ShouldContain("implicit operator string(Sample subject)");
        source.Code.ShouldContain("(string)subject._value");
    }

    private static ConversionsNode.Conversion ConversionAt(Subject subject, int encapsulatedIndex = 0, int conversionIndex = 0)
    {
        var encapsulated = TestSubject.EncapsulatedAt(subject, encapsulatedIndex);
        var conversions = new ConversionsNode(encapsulated, subject, encapsulated._Value.Conversions);

        return new ConversionsNode.Conversion(conversions, conversionIndex, subject, conversions._Value[conversionIndex]);
    }
}