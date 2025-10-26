namespace Monify.Console.Structs.Nested.InRecord.OutterForArrayTests.InnerTests;

public static class WhenEqualityOperatorWithOutterForArrayInnerIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };
    private static readonly int[] DifferentValue = new[] { 4, 5, 6 };

    [Fact]
    public static void GivenBothNullThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner? left = default;
        OutterForArray<int>.Inner? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner? left = default;
        OutterForArray<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner left = new(SampleValue);
        OutterForArray<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner left = new(SampleValue);
        OutterForArray<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }
}