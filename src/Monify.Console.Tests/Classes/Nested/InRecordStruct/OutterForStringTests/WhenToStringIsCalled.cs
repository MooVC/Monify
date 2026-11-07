namespace Monify.Console.Classes.Nested.InRecordStruct.OutterForStringTests;

public sealed class WhenToStringIsCalled
{
    private const string Expected = SampleValue;
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        OutterForString<int>.Inner subject = new(SampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}