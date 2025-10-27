namespace Monify.Console.Records.Nested.InClass.OutterForArrayTests.InnerTests;

public static class WhenEqualityOperatorWithOutterForArrayInnerIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

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
        OutterForArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner left = new(_sampleValue);
        OutterForArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner left = new(_sampleValue);
        OutterForArray<int>.Inner right = new(_differentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }
}