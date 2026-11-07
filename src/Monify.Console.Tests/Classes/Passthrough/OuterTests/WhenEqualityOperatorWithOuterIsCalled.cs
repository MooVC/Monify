namespace Monify.Console.Classes.Passthrough.OuterTests;

public sealed class WhenEqualityOperatorWithOuterIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenBothNullThenReturnTrue()
    {
        // Arrange
        Outer? left = default;
        Outer? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        Outer left = new(SampleValue);
        Outer right = new(DifferentValue);

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
        Outer left = new(leftValue);
        Outer right = new(rightValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        Outer? left = default;
        Outer right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        Outer left = new(SampleValue);
        Outer right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }
}