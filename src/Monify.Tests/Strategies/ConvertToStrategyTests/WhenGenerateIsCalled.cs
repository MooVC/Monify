namespace Monify.Strategies.ConvertToStrategyTests;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenSubjectHasConversionToWhenGenerateIsCalledThenNoSourceIsGenerated()
    {
        // Arrange
        var subject = TestSubject.Create();
        subject.HasConversionTo = true;
        var strategy = new ConvertToStrategy();

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenSubjectLacksConversionToWhenGenerateIsCalledThenSourceIsReturned()
    {
        // Arrange
        var subject = TestSubject.Create();
        var strategy = new ConvertToStrategy();

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe("ConvertTo");
        source.Code.ShouldContain("implicit operator Sample(");
    }
}