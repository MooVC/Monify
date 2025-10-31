namespace Monify.Console.Classes.Nested.InInterface.IOutterForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenGetHashCodeIsCalled
{
    private static readonly ImmutableArray<string> _firstValue = ["Eta", "Theta", "Iota"];
    private static readonly ImmutableArray<string> _secondValue = ["Kappa", "Lambda", "Mu"];

    [Fact]
    public static void GivenSameValuesThenReturnSameHashCode()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner first = new(_firstValue);
        IOutterForImmutableArray<int>.Inner second = new(_firstValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldBe(secondHash);
    }

    [Fact]
    public static void GivenDifferentValuesThenReturnDifferentHashCodes()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner first = new(_firstValue);
        IOutterForImmutableArray<int>.Inner second = new(_secondValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldNotBe(secondHash);
    }
}