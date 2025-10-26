namespace Monify.Console.Classes.Nested.InStruct.OutterForIntTests.InnerTests;

public static class WhenEqualsWithObjectIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenNullThenReturnFalse()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenEquivalentOutterForIntInnerThenReturnTrue()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);
        object other = new OutterForInt<int>.Inner(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentTypeThenThrowInvalidCastException()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);
        object other = string.Empty;

        // Act
        Action act = () => subject.Equals(other);

        // Assert
        _ = Should.Throw<InvalidCastException>(act);
    }
}