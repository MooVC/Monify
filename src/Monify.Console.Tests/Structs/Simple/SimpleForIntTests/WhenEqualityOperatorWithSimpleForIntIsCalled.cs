namespace Monify.Console.Structs.Simple.SimpleForIntTests;

public sealed class WhenEqualityOperatorWithSimpleForIntIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Test]
    public void GivenBothNullThenReturnTrue()
    {
        // Arrange
        SimpleForInt? left = default;
        SimpleForInt? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        SimpleForInt left = new(SampleValue);
        SimpleForInt right = new(DifferentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        SimpleForInt? left = default;
        SimpleForInt right = new(SampleValue);

        // Act
        bool actual = left == right;

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
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }
}