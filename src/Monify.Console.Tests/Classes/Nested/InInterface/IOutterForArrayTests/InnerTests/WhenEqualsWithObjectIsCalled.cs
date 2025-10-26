namespace Monify.Console.Classes.Nested.InInterface.IOutterForArrayTests.InnerTests;

public static class WhenEqualsWithObjectIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };

    [Fact]
    public static void GivenNullThenReturnFalse()
    {
        // Arrange
        IOutterForArray<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenEquivalentIOutterForArrayInnerThenReturnTrue()
    {
        // Arrange
        IOutterForArray<int>.Inner subject = new(SampleValue);
        object other = new IOutterForArray<int>.Inner(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentTypeThenThrowInvalidCastException()
    {
        // Arrange
        IOutterForArray<int>.Inner subject = new(SampleValue);
        object other = string.Empty;

        // Act
        Action act = () => subject.Equals(other);

        // Assert
        _ = Should.Throw<InvalidCastException>(act);
    }
}