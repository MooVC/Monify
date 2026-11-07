namespace Monify.Console.Structs.Simple.SimpleForImmutableArrayTests;

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
        SimpleForImmutableArray subject = new(value);

        // Act
        bool actual = subject == value;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        SimpleForImmutableArray subject = new(_sampleValue);

        // Act
        bool actual = subject == _differentValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForImmutableArray subject = new(_sampleValue);

        // Act
        bool actual = subject == _sampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenSubjectIsNullThenReturnFalse()
    {
        // Arrange
        SimpleForImmutableArray? subject = default;

        // Act
        bool actual = subject == _sampleValue;

        // Assert
        actual.ShouldBeFalse();
    }
}