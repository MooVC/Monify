namespace Monify.Console.Classes.Nested.InRecord.OutterForStringTests;

public sealed class WhenInequalityOperatorWithOutterForStringInnerIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        OutterForString<int>.Inner left = new(SampleValue);
        OutterForString<int>.Inner right = new(DifferentValue);

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
        OutterForString<int>.Inner left = new(leftValue);
        OutterForString<int>.Inner right = new(rightValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        OutterForString<int>.Inner left = new(SampleValue);
        OutterForString<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }
}