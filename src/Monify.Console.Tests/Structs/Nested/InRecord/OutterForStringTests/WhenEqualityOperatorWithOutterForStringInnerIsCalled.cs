namespace Monify.Console.Structs.Nested.InRecord.OutterForStringTests;

public sealed class WhenEqualityOperatorWithOutterForStringInnerIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenBothNullThenReturnTrue()
    {
        // Arrange
        OutterForString<int>.Inner? left = default;
        OutterForString<int>.Inner? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        OutterForString<int>.Inner left = new(SampleValue);
        OutterForString<int>.Inner right = new(DifferentValue);

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
        OutterForString<int>.Inner left = new(leftValue);
        OutterForString<int>.Inner right = new(rightValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        OutterForString<int>.Inner? left = default;
        OutterForString<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForString<int>.Inner left = new(SampleValue);
        OutterForString<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }
}