namespace Monify.Console.Structs.Nested.InInterface.IOutterForStringTests;

public sealed class WhenGetHashCodeIsCalled
{
    private const string FirstValue = "Alpha";
    private const string SecondValue = "Beta";

    [Test]
    public void GivenDifferentValuesThenReturnDifferentHashCodes()
    {
        // Arrange
        IOutterForString<int>.Inner first = new(FirstValue);
        IOutterForString<int>.Inner second = new(SecondValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldNotBe(secondHash);
    }

    [Test]
    public void GivenSameValuesThenReturnSameHashCode()
    {
        // Arrange
        IOutterForString<int>.Inner first = new(FirstValue);
        IOutterForString<int>.Inner second = new(FirstValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldBe(secondHash);
    }
}