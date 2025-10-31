namespace Monify.Console.Records.Simple.SimpleForIntTests;

public static class WhenToStringIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("SimpleForInt { }");
    }
}