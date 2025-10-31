namespace Monify.Console.Structs.Simple.SimpleForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenImplicitOperatorToImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Fact]
    public static void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        SimpleForImmutableArray subject = new(_sampleValue);

        // Act
        ImmutableArray<string> actual = subject;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}