namespace Monify.Console.Structs.Passthrough.OuterTests;

public sealed class WhenEqualsWithObjectIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenDifferentTypeThenReturnsFalse()
    {
        // Arrange
        Outer subject = new(SampleValue);
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
        Outer subject = new(SampleValue);
        object other = new Outer(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenNullThenReturnFalse()
    {
        // Arrange
        Outer subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }
}