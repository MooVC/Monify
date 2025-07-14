namespace Monify.Strategies.ConstructorStrategyTests;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenSubjectHasConstructorWhenGenerateIsCalledThenNoSourceIsGenerated()
    {
        // Arrange
        var subject = TestSubject.Create();
        subject.HasConstructorForEncapsulatedValue = true;
        var strategy = new ConstructorStrategy();

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenSubjectLacksConstructorWhenGenerateIsCalledThenSourceIsReturned()
    {
        // Arrange
        var subject = TestSubject.Create();
        var strategy = new ConstructorStrategy();

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe(ConstructorStrategy.Name);
        source.Code.ShouldContain("public Sample(int value)");
    }
}