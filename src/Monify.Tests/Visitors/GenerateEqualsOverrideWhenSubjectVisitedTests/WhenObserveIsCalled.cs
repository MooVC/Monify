namespace Monify.Visitors.GenerateEqualsOverrideWhenSubjectVisitedTests;

using Monify.Model;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenOverridesWhenAllowedThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.CanOverrideEquals = true;
        var visitor = new GenerateEqualsOverrideWhenSubjectVisited();

        // Act
        Source source = visitor.Observe(new Subject.Graph.Self(subject, subject)).Single();

        // Assert
        source.Hint.ShouldBe(nameof(Equals));
        source.Code.ShouldContain("public override bool Equals(object other)");
    }

    [Test]
    public void GivenOverridesWhenNotAllowedThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var visitor = new GenerateEqualsOverrideWhenSubjectVisited();

        // Act
        IEnumerable<Source> result = visitor.Observe(new Subject.Graph.Self(subject, subject));

        // Assert
        result.ShouldBeEmpty();
    }
}