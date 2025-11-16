namespace Monify.Strategies.EquatableStrategyTests;

using Monify.Model;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenSubjectWhenHasEquatablesThenNoSourceIsGenerated()
    {
        // Arrange
        var strategy = new EquatableStrategy();
        Subject subject = TestSubject.Create();

        subject.Encapsulated = subject.Encapsulated.Clear();
        subject.IsEquatable = true;
        subject.HasEquatable = true;

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenSubjectWhenDoesNotHaveEquatableThenTwoSourcesAreReturned()
    {
        // Arrange
        var strategy = new EquatableStrategy();
        Subject subject = TestSubject.Create();

        subject.Encapsulated = subject.Encapsulated.Clear();

        // Act
        Source[] sources = strategy.Generate(subject).ToArray();

        // Assert
        sources.Length.ShouldBe(2);
        sources[0].Hint.ShouldBe("IEquatable.Self");
        sources[1].Hint.ShouldBe("IEquatable.Self.Equals");
    }
}