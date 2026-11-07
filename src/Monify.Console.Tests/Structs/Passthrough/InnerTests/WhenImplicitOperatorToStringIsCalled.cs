namespace Monify.Console.Structs.Passthrough.InnerTests;

public sealed class WhenImplicitOperatorToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenNullSubjectThenNullIsReturned()
    {
        // Arrange
        Inner? subject = default;

        // Act
        string result = (string)subject!;

        // Assert
        result.ShouldBeNull();
    }

    [Test]
    public void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        Inner subject = new(SampleValue);

        // Act
        string actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}