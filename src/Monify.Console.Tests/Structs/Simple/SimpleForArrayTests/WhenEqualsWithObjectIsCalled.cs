namespace Monify.Console.Structs.Simple.SimpleForArrayTests;

public static class WhenEqualsWithObjectIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Fact]
    public static void GivenNullThenThrowNullReferenceException()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        Action act = () => subject.Equals((object?)default);

        // Assert
        _ = Should.Throw<NullReferenceException>(act);
    }

    [Fact]
    public static void GivenEquivalentSimpleForArrayThenReturnTrue()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);
        object other = new SimpleForArray(_sampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentTypeThenThrowInvalidCastException()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);
        object other = string.Empty;

        // Act
        Action act = () => subject.Equals(other);

        // Assert
        _ = Should.Throw<InvalidCastException>(act);
    }
}