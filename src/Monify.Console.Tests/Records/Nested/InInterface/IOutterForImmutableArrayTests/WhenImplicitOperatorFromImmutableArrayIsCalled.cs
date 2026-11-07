namespace Monify.Console.Records.Nested.InInterface.IOutterForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenImplicitOperatorFromImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner result = _sampleValue;

        // Act
        ImmutableArray<string> actual = result;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}