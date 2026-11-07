namespace Monify.Console.Classes.Simple.SimpleForStringTests;

public sealed class WhenEqualityOperatorWithSimpleForStringIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenBothNullThenReturnTrue()
    {
        // Arrange
        SimpleForString? left = default;
        SimpleForString? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        SimpleForString left = new(SampleValue);
        SimpleForString right = new(DifferentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentValuesThenReturnTrue()
    {
        // Arrange
        string leftValue = new(SampleValue.ToCharArray());
        string rightValue = new(SampleValue.ToCharArray());
        SimpleForString left = new(leftValue);
        SimpleForString right = new(rightValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        SimpleForString? left = default;
        SimpleForString right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForString left = new(SampleValue);
        SimpleForString right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }
}