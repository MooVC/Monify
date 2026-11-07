namespace Monify.Console.Structs.Simple.SimpleForIntTests;

public sealed class WhenEqualsWithSimpleForIntIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        SimpleForInt left = new(SampleValue);
        SimpleForInt right = new(DifferentValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForInt left = new(SampleValue);
        SimpleForInt right = new(SampleValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }
}