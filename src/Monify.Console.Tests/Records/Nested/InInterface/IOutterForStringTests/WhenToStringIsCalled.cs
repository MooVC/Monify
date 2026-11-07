namespace Monify.Console.Records.Nested.InInterface.IOutterForStringTests;

public sealed class WhenToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("Inner { }");
    }
}