namespace Monify.Console.Structs.Simple.SimpleForArrayTests;

public sealed class WhenImplicitOperatorToIntArrayIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        int[] actual = subject;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}