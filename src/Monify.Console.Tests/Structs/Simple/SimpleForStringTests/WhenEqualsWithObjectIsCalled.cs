namespace Monify.Console.Structs.Simple.SimpleForStringTests;

public static class WhenEqualsWithObjectIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenNullThenReturnsFalse()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);

        // Act
        bool result = subject.Equals((object?)default);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public static void GivenEquivalentSimpleForStringThenReturnTrue()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);
        object other = new SimpleForString(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentTypeThenReturnsFalse()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);
        object other = string.Empty;

        // Act
        bool result = subject.Equals(other);

        // Assert
        result.ShouldBeFalse();
    }
}