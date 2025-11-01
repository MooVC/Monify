namespace Monify.Console.Records.Nested.InRecord.OutterForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenEqualityOperatorWithOutterForImmutableArrayInnerIsCalled
{
    private static readonly ImmutableArray<string> _differentValue = ["Delta", "Epsilon", "Zeta"];
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Fact]
    public static void GivenBothNullThenReturnTrue()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner? left = default;
        OutterForImmutableArray<int>.Inner? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner? left = default;
        OutterForImmutableArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner left = new(_sampleValue);
        OutterForImmutableArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenEquivalentValuesThenReturnFalse()
    {
        // Arrange
        ImmutableArray<string> leftValues = ["Alpha", "Beta", "Gamma"];
        ImmutableArray<string> rightValues = ["Alpha", "Beta", "Gamma"];
        OutterForImmutableArray<int>.Inner left = new(leftValues);
        OutterForImmutableArray<int>.Inner right = new(rightValues);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner left = new(_sampleValue);
        OutterForImmutableArray<int>.Inner right = new(_differentValue);

        // Act
        bool actual = left == right;

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
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }
}