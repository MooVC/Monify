namespace Monify.Console.Structs.Simple.SimpleForIntTests;

public static class WhenToStringIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenValueThenThrowFormatException()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        Action act = () => subject.ToString();

        // Assert
        _ = Should.Throw<FormatException>(act);
    }
}