namespace Monify.Console.Classes.Simple.SimpleForStringTests;

public sealed class WhenToStringIsCalled
{
    private const string Expected = SampleValue;
    private const string SampleValue = "Sample";

    [Test]
    public void GivenNullThenEmptyStringIsReturned()
    {
        // Arrange
        SimpleForString subject = new(null!);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(string.Empty);
    }

    [Test]
    public void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}