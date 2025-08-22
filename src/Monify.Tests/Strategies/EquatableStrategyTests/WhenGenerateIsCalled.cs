namespace Monify.Strategies.EquatableStrategyTests;

using Monify.Model;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenConditionsWhenFalseThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var strategy = new EquatableStrategy(_ => false, _ => "true", _ => false, "Self", subject => subject.Qualification);

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenConditionsWhenTrueThenTwoSourcesAreReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var strategy = new EquatableStrategy(_ => true, _ => "true", _ => true, "Self", subject => subject.Qualification);

        // Act
        Source[] sources = strategy.Generate(subject).ToArray();

        // Assert
        sources.Length.ShouldBe(2);
        sources[0].Hint.ShouldBe("IEquatable.Self");
        sources[1].Hint.ShouldBe("IEquatable.Self.Equals");
    }
}