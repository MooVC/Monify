namespace Monify.Console.Records.Nested.InRecord.OutterForArrayTests;

public sealed class WhenConstructorIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenValueThenValueIsStored()
    {
        // Arrange
        OutterForArray<int>.Inner instance = new(_sampleValue);

        // Act
        int[] actual = instance;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}