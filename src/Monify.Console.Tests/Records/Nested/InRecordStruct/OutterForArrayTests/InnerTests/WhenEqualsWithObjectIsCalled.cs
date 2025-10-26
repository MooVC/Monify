namespace Monify.Console.Records.Nested.InRecordStruct.OutterForArrayTests.InnerTests;

public static class WhenEqualsWithObjectIsCalled
{
    private static readonly int[] SampleValue = new[] { 1, 2, 3 };

    [Fact]
    public static void GivenNullThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
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
    public static void GivenDifferentTypeThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(SampleValue);
        object other = string.Empty;

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeFalse();
    }
}