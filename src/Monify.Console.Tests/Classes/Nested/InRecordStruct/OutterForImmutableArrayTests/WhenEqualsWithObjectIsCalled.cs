namespace Monify.Console.Classes.Nested.InRecordStruct.OutterForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenEqualsWithObjectIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenDifferentTypeThenReturnsFalse()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner subject = new(_sampleValue);
        object other = string.Empty;

        // Act
        bool result = subject.Equals(other);

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentOutterForImmutableArrayInnerThenReturnTrue()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner subject = new(_sampleValue);
        object other = new OutterForImmutableArray<int>.Inner(_sampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenNullThenReturnFalse()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }
}