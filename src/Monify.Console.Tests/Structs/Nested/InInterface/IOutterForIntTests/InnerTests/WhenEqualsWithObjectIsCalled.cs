namespace Monify.Console.Structs.Nested.InInterface.IOutterForIntTests.InnerTests;

public static class WhenEqualsWithObjectIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenNullThenThrowNullReferenceException()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        Action act = () => subject.Equals((object?)default);

        // Assert
        _ = Should.Throw<NullReferenceException>(act);
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
    public static void GivenDifferentTypeThenThrowInvalidCastException()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);
        object other = string.Empty;

        // Act
        Action act = () => subject.Equals(other);

        // Assert
        _ = Should.Throw<InvalidCastException>(act);
    }
}