namespace Monify.Console.Classes.Nested.InInterface.IOutterForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenEqualsWithImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _differentValue = ["Delta", "Epsilon", "Zeta"];
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject.Equals(_differentValue);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject.Equals(_sampleValue);

        // Assert
        actual.ShouldBeTrue();
    }
}