namespace Monify.Console.Classes.Simple.SimpleForIntTests;

using Shouldly;
using Xunit;

public static class WhenEqualityOperatorWithSimpleForIntIsCalled
{
    private const int SampleValue = 14;
    private const int DifferentValue = 17;

    [Fact]
    public static void GivenBothNullThenReturnTrue()
    {
        // Arrange
        SimpleForInt? left = null;
        SimpleForInt? right = null;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        SimpleForInt? left = null;
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