namespace Monify.Console.Classes.Nested.InRecord.OutterForStringTests;

public sealed class WhenEqualsWithObjectIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenDifferentTypeThenReturnsFalse()
    {
        // Arrange
        OutterForString<int>.Inner subject = new(SampleValue);
        object other = string.Empty;

        // Act
        bool result = subject.Equals(other);

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentOutterForStringInnerThenReturnTrue()
    {
        // Arrange
        OutterForString<int>.Inner subject = new(SampleValue);
        object other = new OutterForString<int>.Inner(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenNullThenReturnFalse()
    {
        // Arrange
        OutterForString<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }
}