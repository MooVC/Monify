namespace Monify.Console.Structs.Passthrough.InnerTests;

public static class WhenEqualsWithObjectIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenNullThenReturnFalse()
    {
        // Arrange
        Inner subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenEquivalentSimpleForStringThenReturnTrue()
    {
        // Arrange
        Inner subject = new(SampleValue);
        object other = new Inner(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentTypeThenReturnsFalse()
    {
        // Arrange
        Inner subject = new(SampleValue);
        object other = string.Empty;

        // Act
        bool result = subject.Equals(other);

        // Assert
        result.ShouldBeFalse();
    }
}