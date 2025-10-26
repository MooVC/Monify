namespace Monify.Console.Classes.Nested.InInterface.IOutterForIntTests.InnerTests;

public static class WhenEqualityOperatorWithIntIsCalled
{
    private const int SampleValue = 42;
    private const int DifferentValue = 84;

    [Fact]
    public static void GivenSubjectIsNullThenReturnFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner? subject = default;

        // Act
        bool actual = subject == SampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject == SampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject == DifferentValue;

        // Assert
        actual.ShouldBeFalse();
    }
}