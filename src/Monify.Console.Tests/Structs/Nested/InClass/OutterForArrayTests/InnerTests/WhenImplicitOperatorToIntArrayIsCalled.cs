namespace Monify.Console.Structs.Nested.InClass.OutterForArrayTests.InnerTests;

public static class WhenImplicitOperatorToIntArrayIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };

    [Fact]
    public static void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(SampleValue);

        // Act
        int[] actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}