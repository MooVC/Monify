namespace Monify.Visitors.GenerateInequalityOperatorsWhenSubjectVisitedTests;

using Monify.Model;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenSubjectWithOperatorThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.HasInequalityOperator = true;
        var visitor = new GenerateInequalityOperatorsWhenSubjectVisited();

        // Act
        IEnumerable<Source> sources = visitor.Observe(new Subject.Graph(subject));

        // Assert
        sources.ShouldBeEmpty();
    }

    [Test]
    public void GivenSubjectWithoutOperatorThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var visitor = new GenerateInequalityOperatorsWhenSubjectVisited();

        // Act
        Source source = visitor.Observe(new Subject.Graph(subject)).Single();

        // Assert
        source.Hint.ShouldBe("Inequality.Self");
    }
}