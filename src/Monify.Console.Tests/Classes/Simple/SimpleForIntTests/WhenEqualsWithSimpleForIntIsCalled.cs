namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public sealed class WhenEqualsWithSimpleForIntIsCalled
{
    private const int DifferentValue = 5;
    private const int SampleValue = 51;

    [Test]
    public void GivenOtherHasDifferentValueThenReturnFalse()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);
        SimpleForInt other = new(DifferentValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenOtherHasSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);
        SimpleForInt other = new(SampleValue);

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenOtherIsNullThenReturnFalse()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);
        SimpleForInt? other = default;

        // Act
        bool actual = subject.Equals(other);

        // Assert
        actual.ShouldBeFalse();
    }
}