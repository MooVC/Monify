namespace Monify.Console.Records.Simple.SimpleForArrayTests;

public static class WhenConstructorIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };

    [Fact]
    public static void GivenValueThenValueIsStored()
    {
        // Arrange
        SimpleForArray instance = new(SampleValue);

        // Act
        int[] actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}