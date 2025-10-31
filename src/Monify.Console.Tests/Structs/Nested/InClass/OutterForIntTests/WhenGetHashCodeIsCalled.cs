namespace Monify.Console.Structs.Nested.InClass.OutterForIntTests;

public static class WhenGetHashCodeIsCalled
{
    private const int FirstValue = 5;
    private const int SecondValue = 9;

    [Fact]
    public static void GivenSameValuesThenReturnSameHashCode()
    {
        // Arrange
        OutterForInt<int>.Inner first = new(FirstValue);
        OutterForInt<int>.Inner second = new(FirstValue);

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
        OutterForInt<int>.Inner first = new(FirstValue);
        OutterForInt<int>.Inner second = new(SecondValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldNotBe(secondHash);
    }
}
