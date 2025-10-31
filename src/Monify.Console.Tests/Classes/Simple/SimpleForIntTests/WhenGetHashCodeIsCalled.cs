namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public static class WhenGetHashCodeIsCalled
{
    private const int FirstValue = 3;
    private const int SecondValue = 4;

    [Fact]
    public static void GivenSameValuesThenReturnSameHashCode()
    {
        // Arrange
        SimpleForInt first = new(FirstValue);
        SimpleForInt second = new(FirstValue);

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
        SimpleForInt first = new(FirstValue);
        SimpleForInt second = new(SecondValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldNotBe(secondHash);
    }
}