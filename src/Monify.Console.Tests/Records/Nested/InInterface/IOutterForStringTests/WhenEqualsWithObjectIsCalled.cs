namespace Monify.Console.Records.Nested.InInterface.IOutterForStringTests;

public static class WhenEqualsWithObjectIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenNullThenReturnFalse()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenEquivalentIOutterForStringInnerThenReturnTrue()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);
        object other = new IOutterForString<int>.Inner(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentTypeThenReturnFalse()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);
        object other = string.Empty;

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeFalse();
    }
}