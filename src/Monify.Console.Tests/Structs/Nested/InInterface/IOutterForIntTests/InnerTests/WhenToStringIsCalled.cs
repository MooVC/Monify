namespace Monify.Console.Structs.Nested.InInterface.IOutterForIntTests.InnerTests;

public static class WhenToStringIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenValueThenThrowFormatException()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        Action act = () => subject.ToString();

        // Assert
        _ = Should.Throw<FormatException>(act);
    }
}