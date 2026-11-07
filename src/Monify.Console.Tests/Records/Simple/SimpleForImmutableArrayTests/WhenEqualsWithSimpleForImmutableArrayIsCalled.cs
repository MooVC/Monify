namespace Monify.Console.Records.Simple.SimpleForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenEqualsWithSimpleForImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _differentValue = ["Delta", "Epsilon", "Zeta"];
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        SimpleForImmutableArray left = new(_sampleValue);
        SimpleForImmutableArray right = new(_differentValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForImmutableArray left = new(_sampleValue);
        SimpleForImmutableArray right = new(_sampleValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }
}