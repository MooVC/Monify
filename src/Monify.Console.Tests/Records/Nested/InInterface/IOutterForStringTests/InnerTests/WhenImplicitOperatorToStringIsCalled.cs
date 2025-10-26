namespace Monify.Console.Records.Nested.InInterface.IOutterForStringTests.InnerTests;

public static class WhenImplicitOperatorToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenNullSubjectThenThrowsArgumentNullException()
    {
        // Arrange
        IOutterForString<int>.Inner? subject = default;

        // Act
        Action act = () => _ = (string)subject!;

        // Assert
        ArgumentNullException exception = Should.Throw<ArgumentNullException>(act);
        exception.ParamName.ShouldBe("subject");
    }

    [Fact]
    public static void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);

        // Act
        string actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}