namespace Monify.Console.Structs.Simple.SimpleForStringTests;

public static class WhenImplicitOperatorFromStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        SimpleForString result = SampleValue;

        // Act
        string actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}