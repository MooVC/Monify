namespace Monify.Console.Classes.Nested.InRecord.OutterForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenInequalityOperatorWithImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _differentValue = ["Delta", "Epsilon", "Zeta"];
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Fact]
    public static void GivenSubjectIsNullThenReturnTrue()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner? subject = default;

        // Act
        bool actual = subject != _sampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenSameValueThenReturnFalse()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject != _sampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Fact]
    public static void GivenDifferentValueThenReturnTrue()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject != _differentValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Fact]
    public static void GivenUninitializedValuesThenReturnFalse()
    {
        // Arrange
        ImmutableArray<string> values = default;
        OutterForImmutableArray<int>.Inner subject = new(values);

        // Act
        bool actual = subject != values;

        // Assert
        actual.ShouldBeFalse();
    }
}