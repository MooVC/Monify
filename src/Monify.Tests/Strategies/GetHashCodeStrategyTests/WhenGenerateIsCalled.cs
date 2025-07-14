namespace Monify.Strategies.GetHashCodeStrategyTests;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenOverridesNotAllowedWhenGenerateIsCalledThenNoSourceIsGenerated()
    {
        // Arrange
        var subject = TestSubject.Create();
        var strategy = new GetHashCodeStrategy();

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
        subject.CanOverrideGetHashCode = true;
        var strategy = new GetHashCodeStrategy();

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe(nameof(object.GetHashCode));
        source.Code.ShouldContain("public override int GetHashCode()");
    }
}