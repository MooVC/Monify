namespace Monify.Console.Classes.Simple.SimpleForIntTests;

using System;
using Shouldly;
using Xunit;

public static class WhenImplicitOperatorToIntIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenNullSubjectThenThrowsArgumentNullException()
    {
        // Arrange
        SimpleForInt? subject = null;

        // Act
        Action act = () => _ = (int)subject!;

        // Assert
        ArgumentNullException exception = Should.Throw<ArgumentNullException>(act);
        exception.ParamName.ShouldBe("subject");
    }

    [Fact]
    public static void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        int actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}