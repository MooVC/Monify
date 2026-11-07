namespace Monify.Console.Records.Simple.SimpleForIntTests;

public sealed class WhenToStringIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("SimpleForInt { }");
    }
}