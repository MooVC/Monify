namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public static class WhenToStringIsCalled
{
    private const string Expected = "SimpleForInt { 91 }";
    private const int SampleValue = 91;

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