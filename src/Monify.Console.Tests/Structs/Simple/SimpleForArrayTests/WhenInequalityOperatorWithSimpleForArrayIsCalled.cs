namespace Monify.Console.Structs.Simple.SimpleForArrayTests;

public sealed class WhenInequalityOperatorWithSimpleForArrayIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        SimpleForArray left = new(_sampleValue);
        SimpleForArray right = new(_differentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenEquivalentValuesThenReturnFalse()
    {
        // Arrange
        int[] leftValues = [1, 2, 3];
        int[] rightValues = [1, 2, 3];
        SimpleForArray left = new(leftValues);
        SimpleForArray right = new(rightValues);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        SimpleForArray left = new(_sampleValue);
        SimpleForArray right = new(_sampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }
}