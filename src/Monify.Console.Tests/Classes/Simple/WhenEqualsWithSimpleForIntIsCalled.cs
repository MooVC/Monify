namespace Monify.Console.Classes.Simple.SimpleForIntTests;

using Shouldly;
using Xunit;

public static class WhenEqualsWithSimpleForIntIsCalled
{
    private const int SampleValue = 51;
    private const int DifferentValue = 5;

    [Fact]
    public static void GivenOtherHasSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);
        SimpleForInt other = new(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenOtherIsNullThenReturnFalse()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);
        SimpleForInt other = null!;

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenOtherHasDifferentValueThenReturnFalse()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);
        SimpleForInt other = new(DifferentValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeFalse();
    }
}