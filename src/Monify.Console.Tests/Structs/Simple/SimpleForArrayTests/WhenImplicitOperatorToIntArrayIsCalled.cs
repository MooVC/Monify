namespace Monify.Console.Structs.Simple.SimpleForArrayTests;

public static class WhenImplicitOperatorToIntArrayIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };

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