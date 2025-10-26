namespace Monify.Console.Classes.Simple.SimpleForIntTests;

using Shouldly;
using Xunit;

public static class WhenConstructorIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenValueThenValueIsStored()
    {
        // Arrange
        SimpleForInt instance = new(SampleValue);

        // Act
        int actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}