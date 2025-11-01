namespace Monify.Console.Records.Simple.SimpleForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenInequalityOperatorWithSimpleForImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _differentValue = ["Delta", "Epsilon", "Zeta"];
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        SimpleForImmutableArray left = new(_sampleValue);
        SimpleForImmutableArray right = new(_sampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenEquivalentValuesThenReturnTrue()
    {
        // Arrange
        ImmutableArray<string> leftValues = ["Alpha", "Beta", "Gamma"];
        ImmutableArray<string> rightValues = ["Alpha", "Beta", "Gamma"];
        SimpleForImmutableArray left = new(leftValues);
        SimpleForImmutableArray right = new(rightValues);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        SimpleForImmutableArray left = new(_sampleValue);
        SimpleForImmutableArray right = new(_differentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenUninitializedValuesThenReturnFalse()
    {
        // Arrange
        ImmutableArray<string> leftValues = default;
        ImmutableArray<string> rightValues = default;
        SimpleForImmutableArray left = new(leftValues);
        SimpleForImmutableArray right = new(rightValues);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }
}