namespace Monify.Console.Structs.Passthrough.OuterTests;

public sealed class WhenGetHashCodeIsCalled
{
    private const string FirstValue = "Alpha";
    private const string SecondValue = "Beta";

    [Test]
    public void GivenDifferentValuesThenReturnDifferentHashCodes()
    {
        // Arrange
        Outer first = new(FirstValue);
        Outer second = new(SecondValue);

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
        Outer first = new(FirstValue);
        Outer second = new(FirstValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldBe(secondHash);
    }
}