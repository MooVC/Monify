namespace Monify.Console.Structs.Passthrough.InnerTests;

public sealed class WhenInequalityOperatorWithInnerIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        Inner left = new(SampleValue);
        Inner right = new(DifferentValue);

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
        Inner left = new(leftValue);
        Inner right = new(rightValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        Inner left = new(SampleValue);
        Inner right = new(SampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }
}