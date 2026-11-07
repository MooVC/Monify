namespace Monify.Console.Structs.Nested.InInterface.IOutterForArrayTests;

public sealed class WhenInequalityOperatorWithIOutterForArrayInnerIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        IOutterForArray<int>.Inner left = new(_sampleValue);
        IOutterForArray<int>.Inner right = new(_differentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenEquivalentValuesThenReturnFalse()
    {
        // Arrange
        int[] leftValues = [1, 2, 3];
        int[] rightValues = [1, 2, 3];
        IOutterForArray<int>.Inner left = new(leftValues);
        IOutterForArray<int>.Inner right = new(rightValues);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        IOutterForArray<int>.Inner left = new(_sampleValue);
        IOutterForArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }
}