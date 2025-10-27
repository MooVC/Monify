namespace Monify.Console.Records.Simple.SimpleForIntTests;

public static class WhenConstructorIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenValueThenValueIsStored()
    {
        // Arrange
        SimpleForInt instance = new(SampleValue);

        // Act
        int actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}