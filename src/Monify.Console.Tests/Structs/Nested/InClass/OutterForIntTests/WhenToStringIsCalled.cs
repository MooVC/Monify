namespace Monify.Console.Structs.Nested.InClass.OutterForIntTests;

public static class WhenToStringIsCalled
{
    private const string Expected = "Inner { 42 }";
    private const int SampleValue = 42;

    [Fact]
    public static void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}
