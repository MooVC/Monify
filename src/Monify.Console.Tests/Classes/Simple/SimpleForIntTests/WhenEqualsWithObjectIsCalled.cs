namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public static class WhenEqualsWithObjectIsCalled
{
    private const int SampleValue = 28;

    [Fact]
    public static void GivenNullThenReturnFalse()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

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
    public static void GivenDifferentTypeThenReturnsFalse()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);
        object other = string.Empty;

        // Act
        bool result = subject.Equals(other);

        // Assert
        result.ShouldBeFalse();
    }
}