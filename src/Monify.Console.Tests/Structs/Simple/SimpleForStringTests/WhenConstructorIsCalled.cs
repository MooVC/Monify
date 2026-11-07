namespace Monify.Console.Structs.Simple.SimpleForStringTests;

public sealed class WhenConstructorIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValueThenValueIsStored()
    {
        // Arrange
        SimpleForString instance = new(SampleValue);

        // Act
        string actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}