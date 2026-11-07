namespace Monify.Console.Structs.Nested.InInterface.IOutterForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenEqualsWithObjectIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenDifferentTypeThenReturnsFalse()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner subject = new(_sampleValue);
        object other = string.Empty;

        // Act
        bool result = subject.Equals(other);

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentIOutterForImmutableArrayInnerThenReturnTrue()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner subject = new(_sampleValue);
        object other = new IOutterForImmutableArray<int>.Inner(_sampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenNullThenReturnsFalse()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool result = subject.Equals((object?)default);

        // Assert
        result.ShouldBeFalse();
    }
}