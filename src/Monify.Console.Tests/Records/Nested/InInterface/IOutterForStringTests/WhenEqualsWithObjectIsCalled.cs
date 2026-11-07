namespace Monify.Console.Records.Nested.InInterface.IOutterForStringTests;

public sealed class WhenEqualsWithObjectIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenDifferentTypeThenReturnFalse()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);
        object other = string.Empty;

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentIOutterForStringInnerThenReturnTrue()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);
        object other = new IOutterForString<int>.Inner(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenNullThenReturnFalse()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }
}