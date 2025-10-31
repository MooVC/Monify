namespace Monify.Console.Structs.Nested.InInterface.IOutterForArrayTests;

public static class WhenConstructorIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Fact]
    public static void GivenValueThenValueIsStored()
    {
        // Arrange
        IOutterForArray<int>.Inner instance = new(_sampleValue);

        // Act
        int[] actual = instance;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}
