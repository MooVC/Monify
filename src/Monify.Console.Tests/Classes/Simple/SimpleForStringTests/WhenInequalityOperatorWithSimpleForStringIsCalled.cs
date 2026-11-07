namespace Monify.Console.Classes.Simple.SimpleForStringTests;

public sealed class WhenInequalityOperatorWithSimpleForStringIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        SimpleForString left = new(SampleValue);
        SimpleForString right = new(DifferentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenEquivalentValuesThenReturnFalse()
    {
        // Arrange
        string leftValue = new(SampleValue.ToCharArray());
        string rightValue = new(SampleValue.ToCharArray());
        SimpleForString left = new(leftValue);
        SimpleForString right = new(rightValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        SimpleForString left = new(SampleValue);
        SimpleForString right = new(SampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }
}