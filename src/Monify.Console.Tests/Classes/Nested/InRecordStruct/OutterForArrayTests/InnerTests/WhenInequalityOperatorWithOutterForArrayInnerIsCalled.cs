namespace Monify.Console.Classes.Nested.InRecordStruct.OutterForArrayTests.InnerTests;

public static class WhenInequalityOperatorWithOutterForArrayInnerIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };
    private static readonly int[] DifferentValue = new[] { 4, 5, 6 };

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner left = new(SampleValue);
        OutterForArray<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner left = new(SampleValue);
        OutterForArray<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }
}