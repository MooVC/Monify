namespace Monify.Console.Classes.Nested.InInterface.IOutterForIntTests;

public sealed class WhenEqualsWithObjectIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenDifferentTypeThenReturnsFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);
        object other = string.Empty;

        // Act
        bool result = subject.Equals(other);

        // Assert
        result.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentIOutterForIntInnerThenReturnTrue()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);
        object other = new IOutterForInt<int>.Inner(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenNullThenReturnFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject.Equals((object?)default);

        // Assert
        actual.ShouldBeFalse();
    }
}