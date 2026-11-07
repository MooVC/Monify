namespace Monify.Console.Records.Nested.InRecord.OutterForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenEqualsWithObjectIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenDifferentTypeThenReturnFalse()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner subject = new(_sampleValue);
        object other = string.Empty;

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeFalse();
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