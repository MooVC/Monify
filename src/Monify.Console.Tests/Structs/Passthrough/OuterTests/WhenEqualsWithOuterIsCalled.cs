namespace Monify.Console.Structs.Passthrough.OuterTests;

public static class WhenEqualsWithOuterIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        Outer left = new(SampleValue);
        Outer right = new(SampleValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        Outer left = new(SampleValue);
        Outer right = new(DifferentValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeFalse();
    }
}