namespace Monify.Console.Structs.Nested.InInterface.IOutterForStringTests.InnerTests;

public static class WhenEqualsWithObjectIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenNullThenThrowNullReferenceException()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);

        // Act
        Action act = () => subject.Equals((object?)default);

        // Assert
        _ = Should.Throw<NullReferenceException>(act);
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
    public static void GivenDifferentTypeThenThrowInvalidCastException()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);
        object other = string.Empty;

        // Act
        Action act = () => subject.Equals(other);

        // Assert
        _ = Should.Throw<InvalidCastException>(act);
    }
}