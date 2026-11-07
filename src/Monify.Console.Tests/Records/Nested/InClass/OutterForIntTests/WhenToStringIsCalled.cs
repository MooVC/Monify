namespace Monify.Console.Records.Nested.InClass.OutterForIntTests;

public sealed class WhenToStringIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("Inner { }");
    }
}