namespace Monify.Console.Structs.Nested.InStruct.OutterForArrayTests;

public static class WhenInequalityOperatorWithOutterForArrayInnerIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner left = new(_sampleValue);
        OutterForArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenEquivalentValuesThenReturnFalse()
    {
        // Arrange
        int[] leftValues = [1, 2, 3];
        int[] rightValues = [1, 2, 3];
        OutterForArray<int>.Inner left = new(leftValues);
        OutterForArray<int>.Inner right = new(rightValues);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner left = new(_sampleValue);
        OutterForArray<int>.Inner right = new(_differentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }
}
