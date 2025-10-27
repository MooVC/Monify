namespace Monify.Console.Structs.Simple.SimpleForIntTests;

public static class WhenImplicitOperatorToIntIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        int actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}