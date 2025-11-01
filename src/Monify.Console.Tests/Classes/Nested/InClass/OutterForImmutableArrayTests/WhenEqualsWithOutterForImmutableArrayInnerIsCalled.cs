namespace Monify.Console.Classes.Nested.InClass.OutterForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenEqualsWithOutterForImmutableArrayInnerIsCalled
{
    private static readonly ImmutableArray<string> _differentValue = ["Delta", "Epsilon", "Zeta"];
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner left = new(_sampleValue);
        OutterForImmutableArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner left = new(_sampleValue);
        OutterForImmutableArray<int>.Inner right = new(_differentValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenUninitializedValuesThenReturnTrue()
    {
        // Arrange
        ImmutableArray<string> leftValues = default;
        ImmutableArray<string> rightValues = default;
        OutterForImmutableArray<int>.Inner left = new(leftValues);
        OutterForImmutableArray<int>.Inner right = new(rightValues);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }
}