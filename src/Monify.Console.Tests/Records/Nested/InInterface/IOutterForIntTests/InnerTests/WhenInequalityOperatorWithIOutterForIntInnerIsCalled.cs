namespace Monify.Console.Records.Nested.InInterface.IOutterForIntTests.InnerTests;

public static class WhenInequalityOperatorWithIOutterForIntInnerIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner left = new(SampleValue);
        IOutterForInt<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        IOutterForInt<int>.Inner left = new(SampleValue);
        IOutterForInt<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }
}