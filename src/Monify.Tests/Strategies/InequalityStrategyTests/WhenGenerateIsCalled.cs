namespace Monify.Strategies.InequalityStrategyTests;

using Monify.Model;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenConditionWhenFalseThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var strategy = new InequalityStrategy(_ => false, "Self", subject => subject.Qualification);

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenConditionWhenTrueThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var strategy = new InequalityStrategy(_ => true, "Self", subject => subject.Qualification);

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe("Inequality.Self");
        source.Code.ShouldContain("operator !=(");
    }
}