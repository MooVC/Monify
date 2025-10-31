namespace Monify.Console.Structs.Simple.SimpleForIntTests;

public static class WhenImplicitOperatorFromIntIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        SimpleForInt result = SampleValue;

        // Act
        int actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}