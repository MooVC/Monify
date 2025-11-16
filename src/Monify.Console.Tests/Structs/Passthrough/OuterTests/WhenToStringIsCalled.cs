namespace Monify.Console.Structs.Passthrough.OuterTests;

public static class WhenToStringIsCalled
{
    private const string Expected = $"Outer {{ Inner {{ {SampleValue} }} }}";
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        Outer subject = new(SampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}