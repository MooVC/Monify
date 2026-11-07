namespace Monify.Console.Structs.Simple.SimpleForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenEqualityOperatorWithSimpleForImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _differentValue = ["Delta", "Epsilon", "Zeta"];
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenBothNullThenReturnTrue()
    {
        // Arrange
        SimpleForImmutableArray? left = default;
        SimpleForImmutableArray? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        SimpleForImmutableArray left = new(_sampleValue);
        SimpleForImmutableArray right = new(_differentValue);

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
        SimpleForImmutableArray left = new(leftValues);
        SimpleForImmutableArray right = new(rightValues);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        SimpleForImmutableArray? left = default;
        SimpleForImmutableArray right = new(_sampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForImmutableArray left = new(_sampleValue);
        SimpleForImmutableArray right = new(_sampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }
}