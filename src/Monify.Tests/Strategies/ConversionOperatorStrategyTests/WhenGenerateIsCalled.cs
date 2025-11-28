namespace Monify.Strategies.ConversionOperatorStrategyTests;

using System.Collections.Immutable;
using Monify.Model;
using Monify.Strategies;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenNoConversionsThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.Encapsulated = [new Encapsulated { Conversions = ImmutableArray<Conversion>.Empty, Type = "int" }];
        var strategy = new ConversionOperatorStrategy();

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenImplicitConversionFromSubjectThenSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.Encapsulated =
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
        var strategy = new ConversionOperatorStrategy();

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe("ConversionOperators.00");
        source.Code.ShouldContain("implicit operator string(Sample subject)");
        source.Code.ShouldContain("(string)subject._value");
    }

    [Fact]
    public void GivenExplicitConversionToSubjectThenSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.Encapsulated =
        [
            new Encapsulated
            {
                Conversions =
                [
                    new Conversion
                    {
                        IsReturnSubject = true,
                        Operator = "op_Explicit",
                        Parameter = "string",
                        Return = subject.Qualification,
                    },
                ],
                Type = "int",
            },
        ];
        var strategy = new ConversionOperatorStrategy();

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe("ConversionOperators.00");
        source.Code.ShouldContain("explicit operator Sample(string value)");
        source.Code.ShouldContain("new Sample((int)value)");
    }
}
