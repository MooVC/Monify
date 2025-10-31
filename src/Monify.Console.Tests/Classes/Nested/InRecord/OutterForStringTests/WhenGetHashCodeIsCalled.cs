namespace Monify.Console.Classes.Nested.InRecord.OutterForStringTests;

public static class WhenGetHashCodeIsCalled
{
    private const string FirstValue = "Alpha";
    private const string SecondValue = "Beta";

    [Fact]
    public static void GivenSameValuesThenReturnSameHashCode()
    {
        // Arrange
        OutterForString<int>.Inner first = new(FirstValue);
        OutterForString<int>.Inner second = new(FirstValue);

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
        OutterForString<int>.Inner first = new(FirstValue);
        OutterForString<int>.Inner second = new(SecondValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldNotBe(secondHash);
    }
}
