namespace Monify.Console.Classes.Nested.InInterface.IOutterForIntTests;

public static class WhenEqualsWithObjectIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenNullThenReturnFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenEquivalentIOutterForIntInnerThenReturnTrue()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);
        object other = new IOutterForInt<int>.Inner(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentTypeThenReturnsFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);
        object other = string.Empty;

        // Act
        bool result = subject.Equals(other);

        // Assert
        result.ShouldBeFalse();
    }
}