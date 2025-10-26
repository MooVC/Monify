namespace Monify.Console.Structs.Nested.InInterface.IOutterForArrayTests.InnerTests;

public static class WhenImplicitOperatorFromIntArrayIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };

    [Fact]
    public static void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        IOutterForArray<int>.Inner result = SampleValue;

        // Act
        int[] actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}