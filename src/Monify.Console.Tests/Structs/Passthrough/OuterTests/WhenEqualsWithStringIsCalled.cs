namespace Monify.Console.Structs.Passthrough.OuterTests;

public static class WhenEqualsWithStringIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        Outer subject = new(SampleValue);

        // Act
        bool actual = subject.Equals(SampleValue);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        Outer subject = new(SampleValue);

        // Act
        bool actual = subject.Equals(DifferentValue);

        // Assert
        actual.ShouldBeFalse();
    }
}