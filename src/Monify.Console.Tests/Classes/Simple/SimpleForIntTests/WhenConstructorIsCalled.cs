namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public sealed class WhenConstructorIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenValueThenValueIsStored()
    {
        // Arrange
        SimpleForInt instance = new(SampleValue);

        // Act
        int actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}