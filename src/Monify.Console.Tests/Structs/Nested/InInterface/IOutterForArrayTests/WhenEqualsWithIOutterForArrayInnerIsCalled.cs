namespace Monify.Console.Structs.Nested.InInterface.IOutterForArrayTests;

public static class WhenEqualsWithIOutterForArrayInnerIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        IOutterForArray<int>.Inner left = new(_sampleValue);
        IOutterForArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        IOutterForArray<int>.Inner left = new(_sampleValue);
        IOutterForArray<int>.Inner right = new(_differentValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeFalse();
    }
}