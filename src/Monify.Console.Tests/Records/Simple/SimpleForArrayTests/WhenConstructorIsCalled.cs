namespace Monify.Console.Records.Simple.SimpleForArrayTests;

public sealed class WhenConstructorIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenValueThenValueIsStored()
    {
        // Arrange
        SimpleForArray instance = new(_sampleValue);

        // Act
        int[] actual = instance;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}