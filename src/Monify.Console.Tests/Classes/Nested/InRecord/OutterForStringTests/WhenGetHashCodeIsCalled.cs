namespace Monify.Console.Classes.Nested.InRecord.OutterForStringTests;

public sealed class WhenGetHashCodeIsCalled
{
    private const string FirstValue = "Alpha";
    private const string SecondValue = "Beta";

    [Test]
    public void GivenDifferentValuesThenReturnDifferentHashCodes()
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

    [Test]
    public void GivenSameValuesThenReturnSameHashCode()
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
}