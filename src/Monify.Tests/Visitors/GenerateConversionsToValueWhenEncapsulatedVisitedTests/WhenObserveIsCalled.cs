namespace Monify.Visitors.GenerateConversionsToValueWhenEncapsulatedVisitedTests;

using Monify.Model;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenSubjectWhenHasConversionFromThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues = [new Encapsulated { HasConversionFrom = true, Type = "int" }];
        var visitor = new GenerateConversionsToValueWhenEncapsulatedVisited();

        // Act
        IEnumerable<Source> result = visitor.Observe(TestSubject.EncapsulatedAt(subject));

        // Assert
        result.ShouldBeEmpty();
    }

    [Test]
    public void GivenSubjectWhenLacksConversionFromThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var visitor = new GenerateConversionsToValueWhenEncapsulatedVisited();

        // Act
        Source source = visitor.Observe(TestSubject.EncapsulatedAt(subject)).Single();

        // Assert
        source.Hint.ShouldBe("ConvertFrom");
        source.Code.ShouldContain("implicit operator int(");
    }

    [Test]
    public void GivenAdditionalEncapsulatedValueThenHintIncludesPassthrough()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues = [new Encapsulated { Type = "int" }, new Encapsulated { Type = "string" }];
        var visitor = new GenerateConversionsToValueWhenEncapsulatedVisited();

        // Act
        Source source = visitor.Observe(TestSubject.EncapsulatedAt(subject, 1)).Single();

        // Assert
        source.Hint.ShouldBe("ConvertFrom.Passthrough.Level01");
        source.Code.ShouldContain("implicit operator string(");
    }
}