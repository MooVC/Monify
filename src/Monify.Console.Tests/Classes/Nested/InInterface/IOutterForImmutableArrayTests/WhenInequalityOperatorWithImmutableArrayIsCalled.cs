namespace Monify.Console.Classes.Nested.InInterface.IOutterForImmutableArrayTests;

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
        IOutterForImmutableArray<int>.Inner subject = new(value);

        // Act
        bool actual = subject != value;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenDifferentValueThenReturnTrue()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject != _differentValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject != _sampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSubjectIsNullThenReturnTrue()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner? subject = default;

        // Act
        bool actual = subject != _sampleValue;

        // Assert
        actual.ShouldBeTrue();
    }
}