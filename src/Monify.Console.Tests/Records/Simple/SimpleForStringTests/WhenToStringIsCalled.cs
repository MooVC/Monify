namespace Monify.Console.Records.Simple.SimpleForStringTests;

public sealed class WhenToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("SimpleForString { }");
    }
}