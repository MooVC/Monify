namespace Monify.Console.Records.Nested.InStruct.OutterForStringTests;

public sealed class WhenToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        OutterForString<int>.Inner subject = new(SampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("Inner { }");
    }
}