namespace Monify.Console.Classes.Passthrough.InnerTests;

public static class WhenToStringIsCalled
{
    private const string Expected = $"Inner {{ {SampleValue} }}";
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        Inner subject = new(SampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}