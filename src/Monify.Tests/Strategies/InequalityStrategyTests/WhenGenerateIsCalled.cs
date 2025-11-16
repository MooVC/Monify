namespace Monify.Strategies.InequalityStrategyTests;

using System.Collections.Generic;
using System.Linq;
using Monify.Model;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenSubjectWhenOperatorsExistThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.HasInequalityOperatorForSelf = true;
        subject.HasInequalityOperatorForValue = true;
        subject.Operators =
        [
            new Operators { HasInequalityOperator = true, Type = "int" },
            new Operators { HasInequalityOperator = true, Type = "string" },
        ];
        var strategy = new InequalityStrategy();

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenSubjectWhenOperatorsMissingThenSelfAndValueAreGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var strategy = new InequalityStrategy();

        // Act
        Source[] sources = strategy.Generate(subject).ToArray();

        // Assert
        sources.Length.ShouldBe(2);
        sources[0].Hint.ShouldBe("Inequality.Self");
        sources[1].Hint.ShouldBe("Inequality.Value");
    }

    [Fact]
    public void GivenSubjectWithPassthroughConversionThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.Operators = [new Operators { Type = "int" }, new Operators { Type = "string" }];
        var strategy = new InequalityStrategy();

        // Act
        Source[] sources = strategy.Generate(subject).ToArray();

        // Assert
        sources.Length.ShouldBe(3);
        sources[2].Hint.ShouldBe("Inequality.Passthrough");
        sources[2].Code.ShouldContain("operator !=");
    }
}
