namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public static class WhenImplicitOperatorFromIntIsCalled
{
    private const int SampleValue = 77;

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