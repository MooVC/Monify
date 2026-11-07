namespace Monify.Console.Structs.Nested.InRecordStruct.OutterForIntTests;

public sealed class WhenToStringIsCalled
{
    private const string Expected = "42";
    private const int SampleValue = 42;

    [Test]
    public void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}