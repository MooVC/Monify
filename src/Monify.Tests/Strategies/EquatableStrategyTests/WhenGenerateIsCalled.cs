namespace Monify.Strategies.EquatableStrategyTests;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenConditionsAreFalseWhenGenerateIsCalledThenNoSourceIsGenerated()
    {
        // Arrange
        var subject = TestSubject.Create();
        var strategy = new EquatableStrategy(_ => false, _ => "true", _ => false, "Self", s => s.Qualification);

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenConditionsAreTrueWhenGenerateIsCalledThenTwoSourcesAreReturned()
    {
        // Arrange
        var subject = TestSubject.Create();
        var strategy = new EquatableStrategy(_ => true, _ => "true", _ => true, "Self", s => s.Qualification);

        // Act
        Source[] sources = strategy.Generate(subject).ToArray();

        // Assert
        sources.Length.ShouldBe(2);
        sources[0].Hint.ShouldBe("IEquatable.Self");
        sources[1].Hint.ShouldBe("IEquatable.Self.Equals");
    }
}