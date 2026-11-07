namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public sealed class WhenToStringIsCalled
{
    private const string Expected = "91";
    private const int SampleValue = 91;

    [Test]
    public void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}