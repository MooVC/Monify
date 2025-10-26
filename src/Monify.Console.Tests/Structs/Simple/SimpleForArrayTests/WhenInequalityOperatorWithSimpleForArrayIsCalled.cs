namespace Monify.Console.Structs.Simple.SimpleForArrayTests;

public static class WhenInequalityOperatorWithSimpleForArrayIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };
    private static readonly int[] DifferentValue = new[] { 4, 5, 6 };

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        SimpleForArray left = new(SampleValue);
        SimpleForArray right = new(SampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        SimpleForArray left = new(SampleValue);
        SimpleForArray right = new(DifferentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }
}