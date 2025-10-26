namespace Monify.Console.Structs.Nested.InStruct.OutterForArrayTests.InnerTests;

public static class WhenEqualsWithOutterForArrayInnerIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };
    private static readonly int[] DifferentValue = new[] { 4, 5, 6 };

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner left = new(SampleValue);
        OutterForArray<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner left = new(SampleValue);
        OutterForArray<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeFalse();
    }
}