namespace Monify.Console.Structs.Nested.InInterface.IOutterForIntTests;

public static class WhenEqualityOperatorWithIOutterForIntInnerIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Fact]
    public static void GivenBothNullThenReturnTrue()
    {
        // Arrange
        IOutterForInt<int>.Inner? left = default;
        IOutterForInt<int>.Inner? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner? left = default;
        IOutterForInt<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        IOutterForInt<int>.Inner left = new(SampleValue);
        IOutterForInt<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner left = new(SampleValue);
        IOutterForInt<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }
}
