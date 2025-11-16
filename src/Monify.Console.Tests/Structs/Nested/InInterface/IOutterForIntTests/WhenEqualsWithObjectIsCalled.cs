namespace Monify.Console.Structs.Nested.InInterface.IOutterForIntTests;

public static class WhenEqualsWithObjectIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenNullThenReturnsFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool result = subject.Equals((object?)default);

        // Assert
        result.ShouldBeFalse();
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