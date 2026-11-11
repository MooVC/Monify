namespace Monify.Visitors.GenerateValueFieldWhenEncapsulatedVisitedTests;

using Monify.Model;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenSubjectWhenHasFieldThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.HasField = true;
        var visitor = new GenerateValueFieldWhenEncapsulatedVisited();

        // Act
        IEnumerable<Source> result = visitor.Observe(TestSubject.EncapsulatedAt(subject));

        // Assert
        result.ShouldBeEmpty();
    }

    [Test]
    public void GivenSubjectWhenLacksFieldThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var visitor = new GenerateValueFieldWhenEncapsulatedVisited();

        // Act
        Source source = visitor.Observe(TestSubject.EncapsulatedAt(subject)).Single();

        // Assert
        source.Hint.ShouldBe(GenerateValueFieldWhenEncapsulatedVisited.Name);
        source.Code.ShouldContain("private readonly int _value;");
    }
}