namespace Monify.Console.Structs.Passthrough.OuterTests;

public static class WhenEqualsWithObjectIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenNullThenReturnFalse()
    {
        // Arrange
        Outer subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenEquivalentSimpleForStringThenReturnTrue()
    {
        // Arrange
        Outer subject = new(SampleValue);
        object other = new Outer(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentTypeThenReturnsFalse()
    {
        // Arrange
        Outer subject = new(SampleValue);
        object other = string.Empty;

        // Act
        bool result = subject.Equals(other);

        // Assert
        result.ShouldBeFalse();
    }
}