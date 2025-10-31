namespace Monify.Console.Structs.Simple.SimpleForStringTests;

public static class WhenEqualsWithObjectIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenNullThenThrowNullReferenceException()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);

        // Act
        Action act = () => subject.Equals((object?)default);

        // Assert
        _ = Should.Throw<NullReferenceException>(act);
    }

    [Fact]
    public static void GivenEquivalentSimpleForStringThenReturnTrue()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);
        object other = new SimpleForString(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentTypeThenThrowInvalidCastException()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);
        object other = string.Empty;

        // Act
        Action act = () => subject.Equals(other);

        // Assert
        _ = Should.Throw<InvalidCastException>(act);
    }
}