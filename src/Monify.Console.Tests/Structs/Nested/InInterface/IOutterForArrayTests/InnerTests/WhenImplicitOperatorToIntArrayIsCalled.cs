namespace Monify.Console.Structs.Nested.InInterface.IOutterForArrayTests.InnerTests;

public static class WhenImplicitOperatorToIntArrayIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };

    [Fact]
    public static void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        IOutterForArray<int>.Inner subject = new(SampleValue);

        // Act
        int[] actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}