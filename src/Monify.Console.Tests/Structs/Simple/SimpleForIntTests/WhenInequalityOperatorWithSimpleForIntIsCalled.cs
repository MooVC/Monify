namespace Monify.Console.Structs.Simple.SimpleForIntTests;

public sealed class WhenInequalityOperatorWithSimpleForIntIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Test]
    public void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        SimpleForInt left = new(SampleValue);
        SimpleForInt right = new(DifferentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        SimpleForInt left = new(SampleValue);
        SimpleForInt right = new(SampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }
}