namespace Monify.Console.Classes.Simple.SimpleForArrayTests;

public static class WhenInequalityOperatorWithIntArrayIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Fact]
    public static void GivenSubjectIsNullThenReturnTrue()
    {
        // Arrange
        SimpleForArray? subject = default;

        // Act
        bool actual = subject != _sampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        bool actual = subject != _sampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnTrue()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        bool actual = subject != _differentValue;

        // Assert
        actual.ShouldBeTrue();
    }
}