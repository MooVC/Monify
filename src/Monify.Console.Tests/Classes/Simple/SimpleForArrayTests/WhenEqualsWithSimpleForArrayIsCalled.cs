namespace Monify.Console.Classes.Simple.SimpleForArrayTests;

public sealed class WhenEqualsWithSimpleForArrayIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        SimpleForArray left = new(_sampleValue);
        SimpleForArray right = new(_differentValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForArray left = new(_sampleValue);
        SimpleForArray right = new(_sampleValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }
}