namespace Monify.Console.Classes.Simple.SimpleForIntTests;

using System;
using Shouldly;
using Xunit;

public static class WhenEqualsWithObjectIsCalled
{
    private const int SampleValue = 28;

    [Fact]
    public static void GivenNullThenReturnFalse()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)null);

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenEquivalentSimpleForIntThenReturnTrue()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);
        object other = new SimpleForInt(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentTypeThenThrowInvalidCastException()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);
        object other = string.Empty;

        // Act
        Action act = () => subject.Equals(other);

        // Assert
        Should.Throw<InvalidCastException>(act);
    }
}