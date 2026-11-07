namespace Monify.Console.Structs.Simple.SimpleForStringTests;

public sealed class WhenToStringIsCalled
{
    private const string Expected = SampleValue;
    private const string SampleValue = "Sample";

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