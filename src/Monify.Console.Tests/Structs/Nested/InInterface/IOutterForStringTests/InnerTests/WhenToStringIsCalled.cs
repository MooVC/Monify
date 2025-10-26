namespace Monify.Console.Structs.Nested.InInterface.IOutterForStringTests.InnerTests;

public static class WhenToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueThenThrowFormatException()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);

        // Act
        Action act = () => subject.ToString();

        // Assert
        _ = Should.Throw<FormatException>(act);
    }
}