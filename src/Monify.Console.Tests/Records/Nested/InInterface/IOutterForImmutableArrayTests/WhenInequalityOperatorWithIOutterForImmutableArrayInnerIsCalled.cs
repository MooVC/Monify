namespace Monify.Console.Records.Nested.InInterface.IOutterForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenInequalityOperatorWithIOutterForImmutableArrayInnerIsCalled
{
    private static readonly ImmutableArray<string> _differentValue = ["Delta", "Epsilon", "Zeta"];
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner left = new(_sampleValue);
        IOutterForImmutableArray<int>.Inner right = new(_sampleValue);

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
        IOutterForImmutableArray<int>.Inner left = new(leftValues);
        IOutterForImmutableArray<int>.Inner right = new(rightValues);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDefaultValueThenReturnFalse()
    {
        // Arrange
        ImmutableArray<string> defaultValue = default;
        IOutterForImmutableArray<int>.Inner left = new(defaultValue);
        IOutterForImmutableArray<int>.Inner right = new(ImmutableArray<string>.Empty);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner left = new(_sampleValue);
        IOutterForImmutableArray<int>.Inner right = new(_differentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }
}