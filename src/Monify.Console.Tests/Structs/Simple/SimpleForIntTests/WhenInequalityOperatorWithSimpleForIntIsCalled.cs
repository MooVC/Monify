namespace Monify.Console.Structs.Simple.SimpleForIntTests;

public static class WhenInequalityOperatorWithSimpleForIntIsCalled
{
    private const int SampleValue = 42;
    private const int DifferentValue = 84;

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        SimpleForInt left = new(SampleValue);
        SimpleForInt right = new(SampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        SimpleForInt left = new(SampleValue);
        SimpleForInt right = new(DifferentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }
}