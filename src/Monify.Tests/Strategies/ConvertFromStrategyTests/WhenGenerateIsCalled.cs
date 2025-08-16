namespace Monify.Strategies.ConvertFromStrategyTests;

using Monify.Model;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenSubjectWhenHasConversionFromThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.HasConversionFrom = true;
        var strategy = new ConvertFromStrategy();

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenSubjectWhenLacksConversionFromThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var strategy = new ConvertFromStrategy();

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe("ConvertFrom");
        source.Code.ShouldContain("implicit operator int(");
    }
}