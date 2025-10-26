namespace Monify.Console.Records.Simple.SimpleForArrayTests;

public static class WhenEqualityOperatorWithIntArrayIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };
    private static readonly int[] DifferentValue = new[] { 4, 5, 6 };

    [Fact]
    public static void GivenSubjectIsNullThenReturnFalse()
    {
        // Arrange
        SimpleForArray? subject = default;

        // Act
        bool actual = subject == SampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForArray subject = new(SampleValue);

        // Act
        bool actual = subject == SampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        SimpleForArray subject = new(SampleValue);

        // Act
        bool actual = subject == DifferentValue;

        // Assert
        actual.ShouldBeFalse();
    }
}