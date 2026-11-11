namespace Monify.Visitors.GenerateEqualityOperatorsWhenSubjectVisitedTests;

using Monify.Model;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenSubjectWithOperatorThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.HasEqualityOperator = true;
        var visitor = new GenerateEqualityOperatorsWhenSubjectVisited();

        // Act
        IEnumerable<Source> sources = visitor.Observe(new Subject.Graph.Self(subject, subject));

        // Assert
        sources.ShouldBeEmpty();
    }

    [Test]
    public void GivenSubjectWithoutOperatorThenSelfIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var visitor = new GenerateEqualityOperatorsWhenSubjectVisited();

        // Act
        Source source = visitor.Observe(new Subject.Graph.Self(subject, subject)).Single();

        // Assert
        source.Hint.ShouldBe("Equality.Self");
    }
}