namespace Monify.Console.Classes.Simple.SimpleForStringTests;

public static class WhenToStringIsCalled
{
    private const string Expected = SampleValue;
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenNullThenEmptyStringIsReturned()
    {
        // Arrange
        SimpleForString subject = new(null!);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(string.Empty);
    }

    [Fact]
    public static void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}