namespace Monify.Console.Structs.Simple.SimpleForIntTests;

public sealed class WhenGetHashCodeIsCalled
{
    private const int FirstValue = 5;
    private const int SecondValue = 9;

    [Test]
    public void GivenDifferentValuesThenReturnDifferentHashCodes()
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

    [Test]
    public void GivenSameValuesThenReturnSameHashCode()
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
}