namespace Monify.Console.Structs.Passthrough.InnerTests;

public static class WhenEqualityOperatorWithInnerIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Fact]
    public static void GivenBothNullThenReturnTrue()
    {
        // Arrange
        Inner? left = default;
        Inner? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        Inner? left = default;
        Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        Inner left = new(SampleValue);
        Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenEquivalentValuesThenReturnTrue()
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

    [Fact]
    public static void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        Inner left = new(SampleValue);
        Inner right = new(DifferentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }
}