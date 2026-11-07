namespace Monify.Console.Classes.Simple.SimpleForArrayTests;

public sealed class WhenToStringIsCalled
{
    private const string Expected = "System.Int32[]";
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}