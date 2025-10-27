namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public static class WhenInequalityOperatorWithIntIsCalled
{
    private const int DifferentValue = 9;
    private const int SampleValue = 7;

    [Fact]
    public static void GivenSubjectIsNullThenReturnTrue()
    {
        // Arrange
        SimpleForInt? subject = default;

        // Act
        bool actual = subject != SampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        bool actual = subject != SampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnTrue()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        bool actual = subject != DifferentValue;

        // Assert
        actual.ShouldBeTrue();
    }
}