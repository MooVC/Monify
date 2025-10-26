namespace Monify.Console.Structs.Nested.InRecord.OutterForArrayTests.InnerTests;

public static class WhenEqualsWithObjectIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };

    [Fact]
    public static void GivenNullThenThrowNullReferenceException()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(SampleValue);

        // Act
        Action act = () => subject.Equals((object?)default);

        // Assert
        _ = Should.Throw<NullReferenceException>(act);
    }

    [Fact]
    public static void GivenEquivalentOutterForArrayInnerThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(SampleValue);
        object other = new OutterForArray<int>.Inner(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentTypeThenThrowInvalidCastException()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(SampleValue);
        object other = string.Empty;

        // Act
        Action act = () => subject.Equals(other);

        // Assert
        _ = Should.Throw<InvalidCastException>(act);
    }
}