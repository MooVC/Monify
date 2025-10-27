namespace Monify.Console.Structs.Simple.SimpleForArrayTests;

public static class WhenToStringIsCalled
{
    private const string Expected = "SimpleForArray { System.Int32[] }";
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Fact]
    public static void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}