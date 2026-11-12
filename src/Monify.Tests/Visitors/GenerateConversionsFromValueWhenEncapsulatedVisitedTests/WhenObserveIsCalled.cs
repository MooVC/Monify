namespace Monify.Visitors.GenerateConversionsFromValueWhenEncapsulatedVisitedTests;

using Monify.Model;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenSubjectWhenHasConversionToThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues = [new Encapsulated { HasConversionTo = true, Type = "int" }];
        var visitor = new GenerateConversionsFromValueWhenEncapsulatedVisited();

        // Act
        IEnumerable<Source> result = visitor.Observe(TestSubject.EncapsulatedAt(subject));

        // Assert
        result.ShouldBeEmpty();
    }

    [Test]
    public void GivenSubjectWhenLacksConversionToThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var visitor = new GenerateConversionsFromValueWhenEncapsulatedVisited();

        // Act
        Source source = visitor.Observe(TestSubject.EncapsulatedAt(subject)).Single();

        // Assert
        source.Hint.ShouldBe("ConvertTo");
        source.Code.ShouldContain("implicit operator Sample(");
    }

    [Test]
    public void GivenAdditionalEncapsulatedValueThenHintIncludesPassthrough()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues = [new Encapsulated { Type = "int" }, new Encapsulated { Type = "string" }];
        var visitor = new GenerateConversionsFromValueWhenEncapsulatedVisited();

        // Act
        Source source = visitor.Observe(TestSubject.EncapsulatedAt(subject, 1)).Single();

        // Assert
        source.Hint.ShouldBe("ConvertTo.Passthrough.Level01");
        source.Code.ShouldContain("implicit operator Sample(string value)");
    }
}