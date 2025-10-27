namespace Monify.Console.Records.Simple.SimpleForStringTests;

public static class WhenToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("SimpleForString { }");
    }
}