namespace Monify.Console.Classes.Nested.InClass.OutterForArrayTests;

public static class WhenToStringIsCalled
{
    private const string Expected = "Inner { System.Int32[] }";
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Fact]
    public static void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(_sampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}