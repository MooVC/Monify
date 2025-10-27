namespace Monify.Console.Records.Nested.InRecordStruct.OutterForArrayTests.InnerTests;

public static class WhenToStringIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Fact]
    public static void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(_sampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("Inner { }");
    }
}