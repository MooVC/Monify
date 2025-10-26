namespace Monify.Console.Structs.Nested.InInterface.IOutterForArrayTests.InnerTests;

public static class WhenEqualityOperatorWithIOutterForArrayInnerIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };
    private static readonly int[] DifferentValue = new[] { 4, 5, 6 };

    [Fact]
    public static void GivenBothNullThenReturnTrue()
    {
        // Arrange
        IOutterForArray<int>.Inner? left = default;
        IOutterForArray<int>.Inner? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        IOutterForArray<int>.Inner? left = default;
        IOutterForArray<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        IOutterForArray<int>.Inner left = new(SampleValue);
        IOutterForArray<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        IOutterForArray<int>.Inner left = new(SampleValue);
        IOutterForArray<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }
}