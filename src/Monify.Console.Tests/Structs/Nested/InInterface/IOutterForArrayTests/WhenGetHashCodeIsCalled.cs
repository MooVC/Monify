namespace Monify.Console.Structs.Nested.InInterface.IOutterForArrayTests;

public static class WhenGetHashCodeIsCalled
{
    private static readonly int[] _firstValue = [7, 8, 9];
    private static readonly int[] _secondValue = [10, 11, 12];

    [Fact]
    public static void GivenSameValuesThenReturnSameHashCode()
    {
        // Arrange
        IOutterForArray<int>.Inner first = new(_firstValue);
        IOutterForArray<int>.Inner second = new(_firstValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldBe(secondHash);
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnDifferentHashCodes()
    {
        // Arrange
        IOutterForArray<int>.Inner first = new(_firstValue);
        IOutterForArray<int>.Inner second = new(_secondValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldNotBe(secondHash);
    }
}