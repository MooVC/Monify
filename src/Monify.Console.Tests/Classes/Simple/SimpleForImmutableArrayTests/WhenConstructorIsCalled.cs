namespace Monify.Console.Classes.Simple.SimpleForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenConstructorIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Fact]
    public static void GivenValueThenValueIsStored()
    {
        // Arrange
        SimpleForImmutableArray instance = new(_sampleValue);

        // Act
        ImmutableArray<string> actual = instance;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}