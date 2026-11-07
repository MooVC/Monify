namespace Monify.Console.Structs.Passthrough.OuterTests;

public sealed class WhenImplicitOperatorToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenNullSubjectThenNullIsReturned()
    {
        // Arrange
        Outer? subject = default;

        // Act
        string result = (string)subject!;

        // Assert
        result.ShouldBeNull();
    }

    [Test]
    public void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        Outer subject = new(SampleValue);

        // Act
        string actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}