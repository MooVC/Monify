namespace Monify.Visitors.GenerateToStringOverrideWhenSubjectVisitedTests;

using Monify.Model;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenOverridesWhenAllowedThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.CanOverrideToString = true;
        var visitor = new GenerateToStringOverrideWhenSubjectVisited();

        // Act
        Source source = visitor.Observe(new Subject.Graph.Self(subject, subject)).Single();

        // Assert
        source.Hint.ShouldBe(nameof(ToString));
        source.Code.ShouldContain("public override string ToString()");
        source.Code.ShouldContain("if (global::System.Object.ReferenceEquals(_value, null))");
        source.Code.ShouldContain("return string.Empty;");
        source.Code.ShouldContain("return _value.ToString();");
    }

    [Test]
    public void GivenOverridesWhenNotAllowedThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var visitor = new GenerateToStringOverrideWhenSubjectVisited();

        // Act
        IEnumerable<Source> result = visitor.Observe(new Subject.Graph.Self(subject, subject));

        // Assert
        result.ShouldBeEmpty();
    }
}