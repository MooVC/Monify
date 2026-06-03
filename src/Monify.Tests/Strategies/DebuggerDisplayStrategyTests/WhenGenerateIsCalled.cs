namespace Monify.Strategies.DebuggerDisplayStrategyTests;

using Monify.Model;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenDebuggerDisplayWhenAllowedThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var strategy = new DebuggerDisplayStrategy();

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe("DebuggerDisplay");
        source.Code.ShouldContain("[global::System.Diagnostics.DebuggerDisplay(\"{GetDebuggerDisplay(),nq}\")]");
        source.Code.ShouldContain("private string GetDebuggerDisplay()");
        source.Code.ShouldContain("return string.Format(\"Sample {{ {0} }}\", _value);");
    }

    [Fact]
    public void GivenDebuggerDisplayWhenNotAllowedThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.GenerateDebuggerDisplay = false;
        var strategy = new DebuggerDisplayStrategy();

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }
}