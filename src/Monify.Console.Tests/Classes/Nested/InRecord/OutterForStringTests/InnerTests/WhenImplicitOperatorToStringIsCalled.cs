namespace Monify.Console.Classes.Nested.InRecord.OutterForStringTests.InnerTests;

public static class WhenImplicitOperatorToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenNullSubjectThenThrowsArgumentNullException()
    {
        // Arrange
        OutterForString<int>.Inner? subject = default;

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
        OutterForString<int>.Inner subject = new(SampleValue);

        // Act
        string actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}