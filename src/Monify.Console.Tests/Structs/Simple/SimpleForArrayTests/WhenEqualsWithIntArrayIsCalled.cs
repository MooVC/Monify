namespace Monify.Console.Structs.Simple.SimpleForArrayTests;

public sealed class WhenEqualsWithIntArrayIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        bool actual = subject.Equals(_differentValue);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        bool actual = subject.Equals(_sampleValue);

        // Assert
        actual.ShouldBeTrue();
    }
}