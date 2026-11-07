namespace Monify.Console.Records.Simple.SimpleForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenGetHashCodeIsCalled
{
    private static readonly ImmutableArray<string> _firstValue = ["Eta", "Theta", "Iota"];
    private static readonly ImmutableArray<string> _secondValue = ["Kappa", "Lambda", "Mu"];

    [Test]
    public void GivenDifferentValuesThenReturnDifferentHashCodes()
    {
        // Arrange
        SimpleForImmutableArray first = new(_firstValue);
        SimpleForImmutableArray second = new(_secondValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldNotBe(secondHash);
    }

    [Test]
    public void GivenSameValuesThenReturnSameHashCode()
    {
        // Arrange
        SimpleForImmutableArray first = new(_firstValue);
        SimpleForImmutableArray second = new(_firstValue);

        // Act
        int firstHash = first.GetHashCode();
        int secondHash = second.GetHashCode();

        // Assert
        firstHash.ShouldBe(secondHash);
    }
}