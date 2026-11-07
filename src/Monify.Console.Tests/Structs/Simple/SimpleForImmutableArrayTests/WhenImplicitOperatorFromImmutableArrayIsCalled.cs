namespace Monify.Console.Structs.Simple.SimpleForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenImplicitOperatorFromImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        SimpleForImmutableArray result = _sampleValue;

        // Act
        ImmutableArray<string> actual = result;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}