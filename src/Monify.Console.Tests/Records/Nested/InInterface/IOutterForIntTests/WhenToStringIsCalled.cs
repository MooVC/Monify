namespace Monify.Console.Records.Nested.InInterface.IOutterForIntTests;

public sealed class WhenToStringIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("Inner { }");
    }
}