namespace Monify.Console.Records.Simple.SimpleForArrayTests;

public static class WhenEqualityOperatorWithSimpleForArrayIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };
    private static readonly int[] DifferentValue = new[] { 4, 5, 6 };

    [Fact]
    public static void GivenBothNullThenReturnTrue()
    {
        // Arrange
        SimpleForArray? left = default;
        SimpleForArray? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        SimpleForArray? left = default;
        SimpleForArray right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForArray left = new(SampleValue);
        SimpleForArray right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        SimpleForArray left = new(SampleValue);
        SimpleForArray right = new(DifferentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }
}