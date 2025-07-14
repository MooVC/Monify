namespace Monify.Strategies.ToStringStrategyTests;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenOverridesNotAllowedWhenGenerateIsCalledThenNoSourceIsGenerated()
    {
        // Arrange
        var subject = TestSubject.Create();
        var strategy = new ToStringStrategy();

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenOverridesAllowedWhenGenerateIsCalledThenSourceIsReturned()
    {
        // Arrange
        var subject = TestSubject.Create();
        subject.CanOverrideToString = true;
        var strategy = new ToStringStrategy();

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe(nameof(ToString));
        source.Code.ShouldContain("public override string ToString()");
    }
}