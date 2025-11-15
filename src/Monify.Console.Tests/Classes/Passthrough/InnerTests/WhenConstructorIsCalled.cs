namespace Monify.Console.Classes.Passthrough.InnerTests;

public static class WhenConstructorIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueThenValueIsStored()
    {
        // Arrange
        Inner instance = new(SampleValue);

        // Act
        string actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}