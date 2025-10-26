namespace Monify.Console.Classes.Nested.InInterface.IOutterForArrayTests.InnerTests;

public static class WhenGetHashCodeIsCalled
{
    private static readonly int[] FirstValue = new[] { 7, 8, 9 };
    private static readonly int[] SecondValue = new[] { 10, 11, 12 };

    [Fact]
    public static void GivenSameValuesThenReturnSameHashCode()
    {
        // Arrange
        IOutterForArray<int>.Inner first = new(FirstValue);
        IOutterForArray<int>.Inner second = new(FirstValue);

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
        IOutterForArray<int>.Inner first = new(FirstValue);
        IOutterForArray<int>.Inner second = new(SecondValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldNotBe(secondHash);
    }
}