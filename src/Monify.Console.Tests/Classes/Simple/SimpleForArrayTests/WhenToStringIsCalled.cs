namespace Monify.Console.Classes.Simple.SimpleForArrayTests;

public static class WhenToStringIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Fact]
    public static void GivenValueThenThrowFormatException()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        Action act = () => subject.ToString();

        // Assert
        _ = Should.Throw<FormatException>(act);
    }
}