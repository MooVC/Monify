namespace Monify.Console.Records.Simple.SimpleForStringTests;

public sealed class WhenEqualsWithObjectIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenDifferentTypeThenReturnFalse()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);
        object other = string.Empty;

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentSimpleForStringThenReturnTrue()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);
        object other = new SimpleForString(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenNullThenReturnFalse()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }
}