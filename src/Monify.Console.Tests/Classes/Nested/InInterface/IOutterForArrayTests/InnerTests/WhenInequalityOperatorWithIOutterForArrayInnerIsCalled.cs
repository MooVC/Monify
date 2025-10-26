namespace Monify.Console.Classes.Nested.InInterface.IOutterForArrayTests.InnerTests;

public static class WhenInequalityOperatorWithIOutterForArrayInnerIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };
    private static readonly int[] DifferentValue = new[] { 4, 5, 6 };

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        IOutterForArray<int>.Inner left = new(SampleValue);
        IOutterForArray<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        IOutterForArray<int>.Inner left = new(SampleValue);
        IOutterForArray<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }
}