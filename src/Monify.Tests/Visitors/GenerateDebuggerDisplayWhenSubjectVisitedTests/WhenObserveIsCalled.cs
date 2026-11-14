namespace Monify.Visitors.GenerateDebuggerDisplayWhenSubjectVisitedTests;

using Monify.Model;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenDebuggerDisplayWhenAllowedThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var visitor = new GenerateDebuggerDisplayWhenSubjectVisited();

        // Act
        Source source = visitor.Observe(new Subject.Graph(subject)).Single();

        // Assert
        source.Hint.ShouldBe("DebuggerDisplay");
        source.Code.ShouldContain("[global::System.Diagnostics.DebuggerDisplay(\"{GetDebuggerDisplay(),nq}\")]");
        source.Code.ShouldContain("private string GetDebuggerDisplay()");
        source.Code.ShouldContain("return string.Format(\"Sample {{ {0} }}\", _value);");
    }

    [Test]
    public void GivenDebuggerDisplayWhenNotAllowedThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.GenerateDebuggerDisplay = false;
        var visitor = new GenerateDebuggerDisplayWhenSubjectVisited();

        // Act
        IEnumerable<Source> result = visitor.Observe(new Subject.Graph(subject));

        // Assert
        result.ShouldBeEmpty();
    }
}