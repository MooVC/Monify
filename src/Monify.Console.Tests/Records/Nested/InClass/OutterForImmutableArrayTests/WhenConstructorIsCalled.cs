namespace Monify.Console.Records.Nested.InClass.OutterForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenConstructorIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Fact]
    public static void GivenValueThenValueIsStored()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner instance = new(_sampleValue);

        // Act
        ImmutableArray<string> actual = instance;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}