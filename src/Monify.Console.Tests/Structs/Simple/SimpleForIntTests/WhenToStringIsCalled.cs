namespace Monify.Console.Structs.Simple.SimpleForIntTests;

public static class WhenToStringIsCalled
{
    private const string Expected = "SimpleForInt { 42 }";
    private const int SampleValue = 42;

    [Fact]
    public static void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}