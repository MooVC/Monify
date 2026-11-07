namespace Monify.Console.Structs.Nested.InInterface.IOutterForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenEqualityOperatorWithIOutterForImmutableArrayInnerIsCalled
{
    private static readonly ImmutableArray<string> _differentValue = ["Delta", "Epsilon", "Zeta"];
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenBothNullThenReturnTrue()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner? left = default;
        IOutterForImmutableArray<int>.Inner? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner left = new(_sampleValue);
        IOutterForImmutableArray<int>.Inner right = new(_differentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentValuesThenReturnTrue()
    {
        // Arrange
        ImmutableArray<string> leftValues = ["Alpha", "Beta", "Gamma"];
        ImmutableArray<string> rightValues = ["Alpha", "Beta", "Gamma"];
        IOutterForImmutableArray<int>.Inner left = new(leftValues);
        IOutterForImmutableArray<int>.Inner right = new(rightValues);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner? left = default;
        IOutterForImmutableArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner left = new(_sampleValue);
        IOutterForImmutableArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }
}