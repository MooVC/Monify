namespace Monify.Console.Classes.Nested.InInterface.IOutterForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenEqualityOperatorWithImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _differentValue = ["Delta", "Epsilon", "Zeta"];
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenDefaultValueThenReturnTrue()
    {
        // Arrange
        ImmutableArray<string> value = default;
        IOutterForImmutableArray<int>.Inner subject = new(value);

        // Act
        bool actual = subject == value;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject == _differentValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject == _sampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenSubjectIsNullThenReturnFalse()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner? subject = default;

        // Act
        bool actual = subject == _sampleValue;

        // Assert
        actual.ShouldBeFalse();
    }
}