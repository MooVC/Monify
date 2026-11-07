namespace Monify.Console.Structs.Simple.SimpleForStringTests;

public sealed class WhenEqualsWithObjectIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenDifferentTypeThenReturnsFalse()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);
        object other = string.Empty;

        // Act
        bool result = subject.Equals(other);

        // Assert
        result.ShouldBeFalse();
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
    public void GivenNullThenReturnsFalse()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);

        // Act
        bool result = subject.Equals((object?)default);

        // Assert
        result.ShouldBeFalse();
    }
}