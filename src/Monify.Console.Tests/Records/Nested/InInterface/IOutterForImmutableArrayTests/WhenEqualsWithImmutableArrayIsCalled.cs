namespace Monify.Console.Records.Nested.InInterface.IOutterForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenEqualsWithImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _differentValue = ["Delta", "Epsilon", "Zeta"];
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Fact]
    public static void GivenSameValueThenReturnTrue()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject.Equals(_sampleValue);

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject.Equals(_differentValue);

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenUninitializedValuesThenReturnTrue()
    {
        // Arrange
        ImmutableArray<string> values = default;
        IOutterForImmutableArray<int>.Inner subject = new(values);

        // Act
        bool actual = subject.Equals(values);

        // Assert
        actual.ShouldBeTrue();
    }
}