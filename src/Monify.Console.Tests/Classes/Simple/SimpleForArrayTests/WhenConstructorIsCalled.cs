namespace Monify.Console.Classes.Simple.SimpleForArrayTests;

public static class WhenConstructorIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Fact]
    public static void GivenValueThenValueIsStored()
    {
        // Arrange
        SimpleForArray instance = new(_sampleValue);

        // Act
        int[] actual = instance;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}