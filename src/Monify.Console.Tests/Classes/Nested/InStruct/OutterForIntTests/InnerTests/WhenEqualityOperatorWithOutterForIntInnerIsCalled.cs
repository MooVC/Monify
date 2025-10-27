namespace Monify.Console.Classes.Nested.InStruct.OutterForIntTests.InnerTests;

public static class WhenEqualityOperatorWithOutterForIntInnerIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Fact]
    public static void GivenBothNullThenReturnTrue()
    {
        // Arrange
        OutterForInt<int>.Inner? left = default;
        OutterForInt<int>.Inner? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        OutterForInt<int>.Inner? left = default;
        OutterForInt<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForInt<int>.Inner left = new(SampleValue);
        OutterForInt<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        OutterForInt<int>.Inner left = new(SampleValue);
        OutterForInt<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }
}