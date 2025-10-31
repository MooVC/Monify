namespace Monify.Console.Records.Nested.InStruct.OutterForIntTests;

public static class WhenToStringIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("Inner { }");
    }
}