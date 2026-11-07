namespace Monify.Console.Classes.Simple.SimpleForArrayTests;

public sealed class WhenImplicitOperatorFromIntArrayIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        SimpleForArray result = _sampleValue;

        // Act
        int[] actual = result;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}