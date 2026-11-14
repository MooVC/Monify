namespace Monify.Visitors.GenerateEquatableMembersWhenSubjectVisitedTests;

using Monify.Model;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenSubjectWhenDoesNotHaveEquatableThenTwoSourcesAreReturned()
    {
        // Arrange
        var visitor = new GenerateEquatableMembersWhenSubjectVisited();
        Subject subject = TestSubject.Create();

        // Act
        Source[] sources = visitor.Observe(new Subject.Graph(subject)).ToArray();

        // Assert
        sources.Length.ShouldBe(2);
        sources[0].Hint.ShouldBe("IEquatable.Self");
        sources[1].Hint.ShouldBe("IEquatable.Self.Equals");
    }

    [Test]
    public void GivenSubjectWhenHasEquatablesThenNoSourceIsGenerated()
    {
        // Arrange
        var visitor = new GenerateEquatableMembersWhenSubjectVisited();
        Subject subject = TestSubject.Create();
        subject.IsEquatable = true;
        subject.HasEquatable = true;

        // Act
        IEnumerable<Source> result = visitor.Observe(new Subject.Graph(subject));

        // Assert
        result.ShouldBeEmpty();
    }
}