namespace Monify.Console.Records.Nested.InStruct.OutterForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenEqualityOperatorWithOutterForImmutableArrayInnerIsCalled
{
    private static readonly ImmutableArray<string> _differentValue = ["Delta", "Epsilon", "Zeta"];
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenBothNullThenReturnTrue()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner? left = default;
        OutterForImmutableArray<int>.Inner? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner left = new(_sampleValue);
        OutterForImmutableArray<int>.Inner right = new(_differentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentValuesThenReturnFalse()
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

    [Test]
    public void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner? left = default;
        OutterForImmutableArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner left = new(_sampleValue);
        OutterForImmutableArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }
}