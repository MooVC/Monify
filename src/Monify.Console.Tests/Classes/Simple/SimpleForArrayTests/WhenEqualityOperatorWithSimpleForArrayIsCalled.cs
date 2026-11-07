namespace Monify.Console.Classes.Simple.SimpleForArrayTests;

public sealed class WhenEqualityOperatorWithSimpleForArrayIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenBothNullThenReturnTrue()
    {
        // Arrange
        SimpleForArray? left = default;
        SimpleForArray? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        SimpleForArray left = new(_sampleValue);
        SimpleForArray right = new(_differentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentValuesThenReturnTrue()
    {
        // Arrange
        int[] leftValues = [1, 2, 3];
        int[] rightValues = [1, 2, 3];
        SimpleForArray left = new(leftValues);
        SimpleForArray right = new(rightValues);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        SimpleForArray? left = default;
        SimpleForArray right = new(_sampleValue);

        // Act
        bool actual = left == right;

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
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }
}