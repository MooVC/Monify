namespace Monify.Console.Classes.Simple.SimpleForStringTests;

public static class WhenInequalityOperatorWithSimpleForStringIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        SimpleForString left = new(SampleValue);
        SimpleForString right = new(SampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        SimpleForString left = new(SampleValue);
        SimpleForString right = new(DifferentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }
}