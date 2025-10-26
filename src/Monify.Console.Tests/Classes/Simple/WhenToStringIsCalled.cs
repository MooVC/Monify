namespace Monify.Console.Classes.Simple.SimpleForIntTests;

using System;
using Shouldly;
using Xunit;

public static class WhenToStringIsCalled
{
    private const int SampleValue = 91;

    [Fact]
    public static void GivenValueThenThrowFormatException()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        Action act = () => subject.ToString();

        // Assert
        Should.Throw<FormatException>(act);
    }
}