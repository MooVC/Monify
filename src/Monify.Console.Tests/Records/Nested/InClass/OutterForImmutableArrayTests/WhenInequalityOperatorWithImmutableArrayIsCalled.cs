namespace Monify.Console.Records.Nested.InClass.OutterForImmutableArrayTests;

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
    public static void GivenDefaultValueThenReturnFalse()
    {
        // Arrange
        ImmutableArray<string> value = default;
        OutterForImmutableArray<int>.Inner subject = new(value);

        // Act
        bool actual = subject != value;

        // Assert
        actual.ShouldBeFalse();
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
}