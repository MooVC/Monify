namespace Monify.Console.Classes.Nested.InRecordStruct.OutterForStringTests.InnerTests;

public static class WhenToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueThenThrowFormatException()
    {
        // Arrange
        OutterForString<int>.Inner subject = new(SampleValue);

        // Act
        Action act = () => subject.ToString();

        // Assert
        _ = Should.Throw<FormatException>(act);
    }
}