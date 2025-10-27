namespace Monify.Console.Classes.Simple.SimpleForStringTests;

public static class WhenToStringIsCalled
{
    private const string Expected = $"SimpleForString {{ {SampleValue} }}";
    private const string SampleValue = "Sample";

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