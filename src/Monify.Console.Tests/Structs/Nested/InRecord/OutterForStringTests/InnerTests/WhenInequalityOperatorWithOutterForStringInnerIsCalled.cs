namespace Monify.Console.Structs.Nested.InRecord.OutterForStringTests.InnerTests;

public static class WhenInequalityOperatorWithOutterForStringInnerIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        OutterForString<int>.Inner left = new(SampleValue);
        OutterForString<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        OutterForString<int>.Inner left = new(SampleValue);
        OutterForString<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }
}