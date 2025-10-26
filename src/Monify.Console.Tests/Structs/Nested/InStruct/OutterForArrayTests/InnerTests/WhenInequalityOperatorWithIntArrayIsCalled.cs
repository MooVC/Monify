namespace Monify.Console.Structs.Nested.InStruct.OutterForArrayTests.InnerTests;

public static class WhenInequalityOperatorWithIntArrayIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };
    private static readonly int[] DifferentValue = new[] { 4, 5, 6 };

    [Fact]
    public static void GivenSubjectIsNullThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner? subject = default;

        // Act
        bool actual = subject != SampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject != SampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject != DifferentValue;

        // Assert
        actual.ShouldBeTrue();
    }
}