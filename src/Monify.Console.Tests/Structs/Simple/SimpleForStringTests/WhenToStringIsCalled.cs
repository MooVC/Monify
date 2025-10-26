namespace Monify.Console.Structs.Simple.SimpleForStringTests;

public static class WhenToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueThenThrowFormatException()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);

        // Act
        Action act = () => subject.ToString();

        // Assert
        _ = Should.Throw<FormatException>(act);
    }
}