namespace Monify.Console.Records.Nested.InInterface.IOutterForStringTests;

public static class WhenToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("Inner { }");
    }
}