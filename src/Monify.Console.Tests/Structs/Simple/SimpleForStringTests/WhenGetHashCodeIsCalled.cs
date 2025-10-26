namespace Monify.Console.Structs.Simple.SimpleForStringTests;

public static class WhenGetHashCodeIsCalled
{
    private const string FirstValue = "Alpha";
    private const string SecondValue = "Beta";

    [Fact]
    public static void GivenSameValuesThenReturnSameHashCode()
    {
        // Arrange
        SimpleForString first = new(FirstValue);
        SimpleForString second = new(FirstValue);

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
        SimpleForString first = new(FirstValue);
        SimpleForString second = new(SecondValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldNotBe(secondHash);
    }
}