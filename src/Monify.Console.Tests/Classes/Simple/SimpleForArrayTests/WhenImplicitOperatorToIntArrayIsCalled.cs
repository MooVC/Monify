namespace Monify.Console.Classes.Simple.SimpleForArrayTests;

public static class WhenImplicitOperatorToIntArrayIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };

    [Fact]
    public static void GivenNullSubjectThenThrowsArgumentNullException()
    {
        // Arrange
        SimpleForArray? subject = default;

        // Act
        Action act = () => _ = (int[])subject!;

        // Assert
        ArgumentNullException exception = Should.Throw<ArgumentNullException>(act);
        exception.ParamName.ShouldBe("subject");
    }

    [Fact]
    public static void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        SimpleForArray subject = new(SampleValue);

        // Act
        int[] actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}