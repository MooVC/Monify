namespace Monify.Console.Classes.Simple.SimpleForArrayTests;

public sealed class WhenInequalityOperatorWithIntArrayIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenDifferentValueThenReturnTrue()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        bool actual = subject != _differentValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        bool actual = subject != _sampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSubjectIsNullThenReturnTrue()
    {
        // Arrange
        SimpleForArray? subject = default;

        // Act
        bool actual = subject != _sampleValue;

        // Assert
        actual.ShouldBeTrue();
    }
}