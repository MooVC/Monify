namespace Monify.Console.Records.Simple.SimpleForIntTests;

public sealed class WhenEqualsWithObjectIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenDifferentTypeThenReturnFalse()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);
        object other = string.Empty;

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeFalse();
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