namespace Monify.Console.Classes.Nested.InInterface.IOutterForArrayTests.InnerTests;

public static class WhenToStringIsCalled
{
    private const string Expected = "Inner { System.Int32[] }";
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Fact]
    public static void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        IOutterForArray<int>.Inner subject = new(_sampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}