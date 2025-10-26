namespace Monify.Console.Structs.Nested.InClass.OutterForArrayTests.InnerTests;

public static class WhenToStringIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };

    [Fact]
    public static void GivenValueThenThrowFormatException()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(SampleValue);

        // Act
        Action act = () => subject.ToString();

        // Assert
        _ = Should.Throw<FormatException>(act);
    }
}