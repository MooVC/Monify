namespace Monify.Console.Records.Nested.InRecord.OutterForArrayTests.InnerTests;

public static class WhenImplicitOperatorFromIntArrayIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Fact]
    public static void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        OutterForArray<int>.Inner result = _sampleValue;

        // Act
        int[] actual = result;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}