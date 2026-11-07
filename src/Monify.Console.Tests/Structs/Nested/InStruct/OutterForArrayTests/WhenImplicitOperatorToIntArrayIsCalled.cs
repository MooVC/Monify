namespace Monify.Console.Structs.Nested.InStruct.OutterForArrayTests;

public sealed class WhenImplicitOperatorToIntArrayIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(_sampleValue);

        // Act
        int[] actual = subject;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}