namespace Monify.Strategies.EqualsStrategyTests;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenOverridesNotAllowedWhenGenerateIsCalledThenNoSourceIsGenerated()
    {
        // Arrange
        var subject = TestSubject.Create();
        var strategy = new EqualsStrategy();

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
        subject.CanOverrideEquals = true;
        var strategy = new EqualsStrategy();

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe(nameof(object.Equals));
        source.Code.ShouldContain("public override bool Equals(object other)");
    }
}