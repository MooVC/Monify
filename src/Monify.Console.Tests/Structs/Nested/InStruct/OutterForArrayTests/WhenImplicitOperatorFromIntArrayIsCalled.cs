namespace Monify.Console.Structs.Nested.InStruct.OutterForArrayTests;

public sealed class WhenImplicitOperatorFromIntArrayIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        OutterForArray<int>.Inner result = _sampleValue;

        // Act
        int[] actual = result;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}