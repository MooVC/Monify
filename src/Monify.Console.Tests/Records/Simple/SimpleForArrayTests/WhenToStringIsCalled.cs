namespace Monify.Console.Records.Simple.SimpleForArrayTests;

public static class WhenToStringIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Fact]
    public static void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("SimpleForArray { }");
    }
}