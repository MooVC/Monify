namespace Monify.Visitors.GenerateGetHashCodeOverrideWhenSubjectVisitedTests;

using Monify.Model;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenOverridesWhenAllowedThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.CanOverrideGetHashCode = true;
        var visitor = new GenerateGetHashCodeOverrideWhenSubjectVisited();

        // Act
        Source source = visitor.Observe(new Subject.Graph(subject)).Single();

        // Assert
        source.Hint.ShouldBe(nameof(object.GetHashCode));
        source.Code.ShouldContain("public override int GetHashCode()");
    }

    [Test]
    public void GivenOverridesWhenNotAllowedThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var visitor = new GenerateGetHashCodeOverrideWhenSubjectVisited();

        // Act
        IEnumerable<Source> result = visitor.Observe(new Subject.Graph(subject));

        // Assert
        result.ShouldBeEmpty();
    }
}