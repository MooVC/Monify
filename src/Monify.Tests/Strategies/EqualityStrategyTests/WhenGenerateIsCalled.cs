namespace Monify.Strategies.EqualityStrategyTests;

using Monify.Model;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenConditionWhenFalseThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var strategy = new EqualityStrategy(_ => false, "Self", subject => subject.Qualification);

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
        var strategy = new EqualityStrategy(_ => true, "Self", subject => subject.Qualification);

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe("Equality.Self");
        source.Code.ShouldContain("operator ==(");
    }
}