namespace Monify.Console.Classes.Nested.InInterface.IOutterForStringTests;

public sealed class WhenInequalityOperatorWithIOutterForStringInnerIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        IOutterForString<int>.Inner left = new(SampleValue);
        IOutterForString<int>.Inner right = new(DifferentValue);

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
        IOutterForString<int>.Inner left = new(leftValue);
        IOutterForString<int>.Inner right = new(rightValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        IOutterForString<int>.Inner left = new(SampleValue);
        IOutterForString<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }
}