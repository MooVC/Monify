namespace Monify.Console.Structs.Passthrough.InnerTests;

public sealed class WhenConstructorIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValueThenValueIsStored()
    {
        // Arrange
        Inner instance = new(SampleValue);

        // Act
        string actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}