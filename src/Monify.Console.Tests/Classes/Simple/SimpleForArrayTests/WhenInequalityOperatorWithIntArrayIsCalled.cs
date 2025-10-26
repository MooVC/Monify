namespace Monify.Console.Classes.Simple.SimpleForArrayTests;

public static class WhenInequalityOperatorWithIntArrayIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };
    private static readonly int[] DifferentValue = new[] { 4, 5, 6 };

    [Fact]
    public static void GivenSubjectIsNullThenReturnTrue()
    {
        // Arrange
        SimpleForArray? subject = default;

        // Act
        bool actual = subject != SampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        SimpleForArray subject = new(SampleValue);

        // Act
        bool actual = subject != SampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnTrue()
    {
        // Arrange
        SimpleForArray subject = new(SampleValue);

        // Act
        bool actual = subject != DifferentValue;

        // Assert
        actual.ShouldBeTrue();
    }
}