namespace Monify.Console.Records.Nested.InStruct.OutterForStringTests.InnerTests;

public static class WhenToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        OutterForString<int>.Inner subject = new(SampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("Inner { }");
    }
}