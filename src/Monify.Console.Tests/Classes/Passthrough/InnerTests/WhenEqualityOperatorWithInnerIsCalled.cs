namespace Monify.Console.Classes.Passthrough.InnerTests;

public sealed class WhenEqualityOperatorWithInnerIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenBothNullThenReturnTrue()
    {
        // Arrange
        Inner? left = default;
        Inner? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        Inner left = new(SampleValue);
        Inner right = new(DifferentValue);

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
        Inner left = new(leftValue);
        Inner right = new(rightValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        Inner? left = default;
        Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        Inner left = new(SampleValue);
        Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }
}