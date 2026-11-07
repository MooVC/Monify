namespace Monify.Console.Records.Simple.SimpleForStringTests;

public sealed class WhenImplicitOperatorToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenNullSubjectThenThrowsArgumentNullException()
    {
        // Arrange
        SimpleForString? subject = default;

        // Act
        Action act = () => _ = (string)subject!;

        // Assert
        ArgumentNullException exception = Should.Throw<ArgumentNullException>(act);
        exception.ParamName.ShouldBe(nameof(subject));
    }

    [Test]
    public void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);

        // Act
        string actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}