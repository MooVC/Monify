namespace Monify.Console.Records.Nested.InStruct.OutterForArrayTests.InnerTests;

public static class WhenToStringIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };

    [Fact]
    public static void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(SampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("Inner { }");
    }
}