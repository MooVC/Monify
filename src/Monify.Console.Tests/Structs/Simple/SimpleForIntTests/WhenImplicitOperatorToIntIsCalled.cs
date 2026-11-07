namespace Monify.Console.Structs.Simple.SimpleForIntTests;

public sealed class WhenImplicitOperatorToIntIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        int actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}