namespace Monify.Console.Records.Nested.InRecord.OutterForIntTests.InnerTests;

public static class WhenEqualsWithOutterForIntInnerIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForInt<int>.Inner left = new(SampleValue);
        OutterForInt<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        OutterForInt<int>.Inner left = new(SampleValue);
        OutterForInt<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeFalse();
    }
}