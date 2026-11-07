namespace Monify.Console.Records.Nested.InRecordStruct.OutterForArrayTests;

public sealed class WhenEqualsWithObjectIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenDifferentTypeThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(_sampleValue);
        object other = string.Empty;

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeFalse();
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
    public void GivenNullThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }
}