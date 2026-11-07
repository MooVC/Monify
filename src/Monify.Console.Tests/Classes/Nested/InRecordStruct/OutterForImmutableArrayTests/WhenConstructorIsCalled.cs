namespace Monify.Console.Classes.Nested.InRecordStruct.OutterForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenConstructorIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenValueThenValueIsStored()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner instance = new(_sampleValue);

        // Act
        ImmutableArray<string> actual = instance;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}