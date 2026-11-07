namespace Monify.Console.Classes.Nested.InClass.OutterForIntTests;

public sealed class WhenImplicitOperatorToIntIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenNullSubjectThenThrowsArgumentNullException()
    {
        // Arrange
        OutterForInt<int>.Inner? subject = default;

        // Act
        Action act = () => _ = (int)subject!;

        // Assert
        ArgumentNullException exception = Should.Throw<ArgumentNullException>(act);
        exception.ParamName.ShouldBe(nameof(subject));
    }

    [Test]
    public void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        int actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}