namespace Monify.Console.Records.Simple.SimpleForIntTests;

public static class WhenEqualityOperatorWithSimpleForIntIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Fact]
    public static void GivenBothNullThenReturnTrue()
    {
        // Arrange
        SimpleForInt? left = default;
        SimpleForInt? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        SimpleForInt? left = default;
        SimpleForInt right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForInt left = new(SampleValue);
        SimpleForInt right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        SimpleForInt left = new(SampleValue);
        SimpleForInt right = new(DifferentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }
}