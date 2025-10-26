namespace Monify.Console.Structs.Nested.InClass.OutterForIntTests.InnerTests;

public static class WhenEqualityOperatorWithIntIsCalled
{
    private const int SampleValue = 42;
    private const int DifferentValue = 84;

    [Fact]
    public static void GivenSubjectIsNullThenReturnFalse()
    {
        // Arrange
        OutterForInt<int>.Inner? subject = default;

        // Act
        bool actual = subject == SampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject == SampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject == DifferentValue;

        // Assert
        actual.ShouldBeFalse();
    }
}