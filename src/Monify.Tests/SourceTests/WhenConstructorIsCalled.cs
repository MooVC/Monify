namespace Monify.SourceTests;

public sealed class WhenConstructorIsCalled
{
    [Fact]
    public void GivenCodeAndHintWhenConstructorIsCalledThenPropertiesAreSet()
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
}