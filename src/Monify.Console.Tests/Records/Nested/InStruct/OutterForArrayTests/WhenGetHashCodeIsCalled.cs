namespace Monify.Console.Records.Nested.InStruct.OutterForArrayTests;

public static class WhenGetHashCodeIsCalled
{
    private static readonly int[] _firstValue = [7, 8, 9];
    private static readonly int[] _secondValue = [10, 11, 12];

    [Fact]
    public static void GivenSameValuesThenReturnSameHashCode()
    {
        // Arrange
        OutterForArray<int>.Inner first = new(_firstValue);
        OutterForArray<int>.Inner second = new(_firstValue);

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
        OutterForArray<int>.Inner first = new(_firstValue);
        OutterForArray<int>.Inner second = new(_secondValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldNotBe(secondHash);
    }
}