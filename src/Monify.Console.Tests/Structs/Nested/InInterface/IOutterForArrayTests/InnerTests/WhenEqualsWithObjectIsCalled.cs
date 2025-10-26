namespace Monify.Console.Structs.Nested.InInterface.IOutterForArrayTests.InnerTests;

public static class WhenEqualsWithObjectIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };

    [Fact]
    public static void GivenNullThenThrowNullReferenceException()
    {
        // Arrange
        IOutterForArray<int>.Inner subject = new(SampleValue);

        // Act
        Action act = () => subject.Equals((object?)default);

        // Assert
        _ = Should.Throw<NullReferenceException>(act);
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