namespace Monify.Console.Classes.Simple.SimpleForStringTests;

public static class WhenConstructorIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueThenValueIsStored()
    {
        // Arrange
        SimpleForString instance = new(SampleValue);

        // Act
        string actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}