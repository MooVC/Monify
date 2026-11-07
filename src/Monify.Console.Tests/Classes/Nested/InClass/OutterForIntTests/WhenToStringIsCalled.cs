namespace Monify.Console.Classes.Nested.InClass.OutterForIntTests;

public sealed class WhenToStringIsCalled
{
    private const string Expected = "42";
    private const int SampleValue = 42;

    [Test]
    public void GivenValueThenTheExpectedStringIsReturned()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}