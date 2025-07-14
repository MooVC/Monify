namespace Monify.Strategies.InequalityStrategyTests;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenConditionIsFalseWhenGenerateIsCalledThenNoSourceIsGenerated()
    {
        // Arrange
        var subject = TestSubject.Create();
        var strategy = new InequalityStrategy(_ => false, "Self", s => s.Qualification);

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenConditionIsTrueWhenGenerateIsCalledThenSourceIsReturned()
    {
        // Arrange
        var subject = TestSubject.Create();
        var strategy = new InequalityStrategy(_ => true, "Self", s => s.Qualification);

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe("Inequality.Self");
        source.Code.ShouldContain("operator !=(");
    }
}