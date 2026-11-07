namespace Monify.Console.Classes.Passthrough.OuterTests;

public sealed class WhenConstructorIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValueThenValueIsStored()
    {
        // Arrange
        Outer instance = new(SampleValue);

        // Act
        string actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}