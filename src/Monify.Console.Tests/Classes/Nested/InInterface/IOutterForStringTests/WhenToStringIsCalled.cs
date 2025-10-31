namespace Monify.Console.Classes.Nested.InInterface.IOutterForStringTests;

public static class WhenToStringIsCalled
{
    private const string Expected = $"Inner {{ {SampleValue} }}";
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}
