namespace Monify.Console.Classes.Simple.SimpleForIntTests;

using Shouldly;
using Xunit;

public static class WhenEqualityOperatorWithIntIsCalled
{
    private const int SampleValue = 101;
    private const int DifferentValue = 202;

    [Fact]
    public static void GivenSubjectIsNullThenReturnFalse()
    {
        // Arrange
        SimpleForInt subject = null!;

        // Act
        bool actual = subject == SampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        bool actual = subject == SampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        bool actual = subject == DifferentValue;

        // Assert
        actual.ShouldBeFalse();
    }
}