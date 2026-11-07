namespace Monify.Console.Records.Simple.SimpleForArrayTests;

public sealed class WhenEqualsWithObjectIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenDifferentTypeThenReturnFalse()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);
        object other = string.Empty;

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentSimpleForArrayThenReturnTrue()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);
        object other = new SimpleForArray(_sampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenNullThenReturnFalse()
    {
        // Arrange
        SimpleForArray subject = new(_sampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }
}