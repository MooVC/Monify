namespace Monify.Console.Structs.Passthrough.OuterTests;

public sealed class WhenInequalityOperatorWithOuterIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        Outer left = new(SampleValue);
        Outer right = new(DifferentValue);

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
        Outer left = new(leftValue);
        Outer right = new(rightValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        Outer left = new(SampleValue);
        Outer right = new(SampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }
}