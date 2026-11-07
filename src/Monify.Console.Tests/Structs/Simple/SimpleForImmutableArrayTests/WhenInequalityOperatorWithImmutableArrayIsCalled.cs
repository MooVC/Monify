namespace Monify.Console.Structs.Simple.SimpleForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenInequalityOperatorWithImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _differentValue = ["Delta", "Epsilon", "Zeta"];
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenDefaultValueThenReturnFalse()
    {
        // Arrange
        ImmutableArray<string> value = default;
        SimpleForImmutableArray subject = new(value);

        // Act
        bool actual = subject != value;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenDifferentValueThenReturnTrue()
    {
        // Arrange
        SimpleForImmutableArray subject = new(_sampleValue);

        // Act
        bool actual = subject != _differentValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        SimpleForImmutableArray subject = new(_sampleValue);

        // Act
        bool actual = subject != _sampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSubjectIsNullThenReturnTrue()
    {
        // Arrange
        SimpleForImmutableArray? subject = default;

        // Act
        bool actual = subject != _sampleValue;

        // Assert
        actual.ShouldBeTrue();
    }
}