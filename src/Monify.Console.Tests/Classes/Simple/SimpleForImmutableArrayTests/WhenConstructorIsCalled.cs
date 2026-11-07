namespace Monify.Console.Classes.Simple.SimpleForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenConstructorIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenValueThenValueIsStored()
    {
        // Arrange
        SimpleForImmutableArray instance = new(_sampleValue);

        // Act
        ImmutableArray<string> actual = instance;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}