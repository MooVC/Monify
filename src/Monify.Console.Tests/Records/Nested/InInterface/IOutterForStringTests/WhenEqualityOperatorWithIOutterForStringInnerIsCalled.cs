namespace Monify.Console.Records.Nested.InInterface.IOutterForStringTests;

public sealed class WhenEqualityOperatorWithIOutterForStringInnerIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenBothNullThenReturnTrue()
    {
        // Arrange
        IOutterForString<int>.Inner? left = default;
        IOutterForString<int>.Inner? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        IOutterForString<int>.Inner left = new(SampleValue);
        IOutterForString<int>.Inner right = new(DifferentValue);

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
        IOutterForString<int>.Inner left = new(leftValue);
        IOutterForString<int>.Inner right = new(rightValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        IOutterForString<int>.Inner? left = default;
        IOutterForString<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        IOutterForString<int>.Inner left = new(SampleValue);
        IOutterForString<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }
}