namespace Monify.Strategies.ToStringStrategyTests;

using Monify.Model;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenOverridesWhenNotAllowedThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var strategy = new ToStringStrategy();

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenOverridesWhenAllowedThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.CanOverrideToString = true;
        var strategy = new ToStringStrategy();

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe(nameof(ToString));
        source.Code.ShouldContain("public override string ToString()");
    }
}