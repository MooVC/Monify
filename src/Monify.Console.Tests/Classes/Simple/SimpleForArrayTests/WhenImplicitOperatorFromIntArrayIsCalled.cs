namespace Monify.Console.Classes.Simple.SimpleForArrayTests;

public static class WhenImplicitOperatorFromIntArrayIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };

    [Fact]
    public static void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        SimpleForArray result = SampleValue;

        // Act
        int[] actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}