namespace Monify.Console.Classes.Nested.InInterface.IOutterForIntTests.InnerTests;

public static class WhenEqualsWithIOutterForIntInnerIsCalled
{
    private const int SampleValue = 42;
    private const int DifferentValue = 84;

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        IOutterForInt<int>.Inner left = new(SampleValue);
        IOutterForInt<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner left = new(SampleValue);
        IOutterForInt<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeFalse();
    }
}