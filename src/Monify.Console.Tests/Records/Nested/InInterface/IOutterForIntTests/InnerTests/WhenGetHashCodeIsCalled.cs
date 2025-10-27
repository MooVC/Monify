namespace Monify.Console.Records.Nested.InInterface.IOutterForIntTests.InnerTests;

public static class WhenGetHashCodeIsCalled
{
    private const int FirstValue = 5;
    private const int SecondValue = 9;

    [Fact]
    public static void GivenSameValuesThenReturnSameHashCode()
    {
        // Arrange
        IOutterForInt<int>.Inner first = new(FirstValue);
        IOutterForInt<int>.Inner second = new(FirstValue);

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
        IOutterForInt<int>.Inner first = new(FirstValue);
        IOutterForInt<int>.Inner second = new(SecondValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldNotBe(secondHash);
    }
}