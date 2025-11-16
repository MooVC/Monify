namespace Monify.Strategies.EqualityStrategyTests;

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
        subject.HasEqualityOperatorForSelf = true;
        subject.HasEqualityOperatorForValue = true;
        subject.Operators =
        [
            new Operators { HasEqualityOperator = true, Type = "int" },
            new Operators { HasEqualityOperator = true, Type = "string" },
        ];
        var strategy = new EqualityStrategy();

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
        var strategy = new EqualityStrategy();

        // Act
        Source[] sources = strategy.Generate(subject).ToArray();

        // Assert
        sources.Length.ShouldBe(2);
        sources[0].Hint.ShouldBe("Equality.Self");
        sources[1].Hint.ShouldBe("Equality.Value");
    }

    [Fact]
    public void GivenSubjectWithPassthroughConversionThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.Operators = [new Operators { Type = "int" }, new Operators { Type = "string" }];
        var strategy = new EqualityStrategy();

        // Act
        Source[] sources = strategy.Generate(subject).ToArray();

        // Assert
        sources.Length.ShouldBe(3);
        sources[2].Hint.ShouldBe("Equality.Passthrough");
        sources[2].Code.ShouldContain("(int)right");
    }
}
