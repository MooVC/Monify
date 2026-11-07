namespace Monify.Console.Records.Nested.InRecordStruct.OutterForArrayTests;

public sealed class WhenGetHashCodeIsCalled
{
    private static readonly int[] _firstValue = [7, 8, 9];
    private static readonly int[] _secondValue = [10, 11, 12];

    [Test]
    public void GivenDifferentValuesThenReturnDifferentHashCodes()
    {
        // Arrange
        OutterForArray<int>.Inner first = new(_firstValue);
        OutterForArray<int>.Inner second = new(_secondValue);

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
        OutterForArray<int>.Inner first = new(_firstValue);
        OutterForArray<int>.Inner second = new(_firstValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldBe(secondHash);
    }
}