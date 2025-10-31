namespace Monify.Console.Records.Simple.SimpleForArrayTests;

public static class WhenInequalityOperatorWithSimpleForArrayIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        SimpleForArray left = new(_sampleValue);
        SimpleForArray right = new(_sampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenEquivalentValuesThenReturnTrue()
    {
        // Arrange
        int[] leftValues = [1, 2, 3];
        int[] rightValues = [1, 2, 3];
        SimpleForArray left = new(leftValues);
        SimpleForArray right = new(rightValues);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        SimpleForArray left = new(_sampleValue);
        SimpleForArray right = new(_differentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }
}