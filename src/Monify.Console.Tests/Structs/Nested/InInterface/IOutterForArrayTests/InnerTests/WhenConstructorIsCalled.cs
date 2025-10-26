namespace Monify.Console.Structs.Nested.InInterface.IOutterForArrayTests.InnerTests;

public static class WhenConstructorIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };

    [Fact]
    public static void GivenValueThenValueIsStored()
    {
        // Arrange
        IOutterForArray<int>.Inner instance = new(SampleValue);

        // Act
        int[] actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}