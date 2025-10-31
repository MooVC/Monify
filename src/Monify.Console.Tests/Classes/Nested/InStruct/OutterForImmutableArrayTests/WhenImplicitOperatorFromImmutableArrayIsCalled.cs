namespace Monify.Console.Classes.Nested.InStruct.OutterForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenImplicitOperatorFromImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Fact]
    public static void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner result = _sampleValue;

        // Act
        ImmutableArray<string> actual = result;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}