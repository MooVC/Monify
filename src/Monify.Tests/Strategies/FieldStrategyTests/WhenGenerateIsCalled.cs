namespace Monify.Strategies.FieldStrategyTests;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenSubjectHasFieldWhenGenerateIsCalledThenNoSourceIsGenerated()
    {
        // Arrange
        var subject = TestSubject.Create();
        subject.HasFieldForEncapsulatedValue = true;
        var strategy = new FieldStrategy();

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenSubjectLacksFieldWhenGenerateIsCalledThenSourceIsReturned()
    {
        // Arrange
        var subject = TestSubject.Create();
        var strategy = new FieldStrategy();

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe(FieldStrategy.Name);
        source.Code.ShouldContain("private readonly int _value;");
    }
}