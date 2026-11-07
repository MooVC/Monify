namespace Monify.Console.Structs.Nested.InStruct.OutterForArrayTests;

public sealed class WhenEqualsWithObjectIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenDifferentTypeThenReturnsFalse()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(_sampleValue);
        object other = string.Empty;

        // Act
        bool result = subject.Equals(other);

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentOutterForArrayInnerThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(_sampleValue);
        object other = new OutterForArray<int>.Inner(_sampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenNullThenReturnsFalse()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool result = subject.Equals((object?)default);

        // Assert
        result.ShouldBeFalse();
    }
}