namespace Monify.SourceTests;

public sealed class WhenConstructed
{
    [Fact]
    public void GivenCodeAndHintThenPropertiesAreSet()
    {
        // Arrange
        const string code = "code";
        const string hint = "hint";

        // Act
        var source = new Source(code, hint);

        // Assert
        source.Code.ShouldBe(code);
        source.Hint.ShouldBe(hint);
    }

    [Fact]
    public void GivenCodeWithWindowsLineEndingsThenCodeIsNormalized()
    {
        // Arrange
        const string code = "line1\r\nline2\rline3";
        const string expected = "line1\nline2\nline3";
        const string hint = "hint";

        // Act
        var source = new Source(code, hint);

        // Assert
        source.Code.ShouldBe(expected);
    }
}