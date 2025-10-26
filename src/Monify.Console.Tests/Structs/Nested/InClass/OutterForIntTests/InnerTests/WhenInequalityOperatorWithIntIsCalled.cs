namespace Monify.Console.Structs.Nested.InClass.OutterForIntTests.InnerTests;

public static class WhenInequalityOperatorWithIntIsCalled
{
    private const int SampleValue = 42;
    private const int DifferentValue = 84;

    [Fact]
    public static void GivenSubjectIsNullThenReturnTrue()
    {
        // Arrange
        OutterForInt<int>.Inner? subject = default;

        // Act
        bool actual = subject != SampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject != SampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnTrue()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject != DifferentValue;

        // Assert
        actual.ShouldBeTrue();
    }
}