namespace Monify.Console.Records.Simple.SimpleForArrayTests;

public sealed class WhenEqualityOperatorWithIntArrayIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        bool actual = subject == _differentValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        bool actual = subject == _sampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenSubjectIsNullThenReturnFalse()
    {
        // Arrange
        SimpleForArray? subject = default;

        // Act
        bool actual = subject == _sampleValue;

        // Assert
        actual.ShouldBeFalse();
    }
}