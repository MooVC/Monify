namespace Monify.Console.Classes.Simple.SimpleForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenEqualsWithObjectIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Fact]
    public static void GivenNullThenReturnFalse()
    {
        // Arrange
        SimpleForImmutableArray subject = new(_sampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenEquivalentSimpleForImmutableArrayThenReturnTrue()
    {
        // Arrange
        SimpleForImmutableArray subject = new(_sampleValue);
        object other = new SimpleForImmutableArray(_sampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentTypeThenReturnsFalse()
    {
        // Arrange
        SimpleForImmutableArray subject = new(_sampleValue);
        object other = string.Empty;

        // Act
        bool result = subject.Equals(other);

        // Assert
        result.ShouldBeFalse();
    }
}