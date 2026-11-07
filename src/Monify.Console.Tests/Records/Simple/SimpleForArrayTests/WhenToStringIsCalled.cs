namespace Monify.Console.Records.Simple.SimpleForArrayTests;

public sealed class WhenToStringIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("SimpleForArray { }");
    }
}