namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public sealed class WhenEqualsWithObjectIsCalled
{
    private const int SampleValue = 28;

    [Test]
    public void GivenDifferentTypeThenReturnsFalse()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);
        object other = string.Empty;

        // Act
        bool result = subject.Equals(other);

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentSimpleForIntThenReturnTrue()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);
        object other = new SimpleForInt(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenNullThenReturnFalse()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }
}