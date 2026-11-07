namespace Monify.Console.Structs.Nested.InClass.OutterForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenInequalityOperatorWithOutterForImmutableArrayInnerIsCalled
{
    private static readonly ImmutableArray<string> _differentValue = ["Delta", "Epsilon", "Zeta"];
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner left = new(_sampleValue);
        OutterForImmutableArray<int>.Inner right = new(_differentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
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
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner left = new(_sampleValue);
        OutterForImmutableArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }
}