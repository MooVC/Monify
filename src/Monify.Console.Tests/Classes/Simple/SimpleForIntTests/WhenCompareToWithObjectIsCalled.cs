namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public sealed class WhenCompareToWithObjectIsCalled
{
    private const int SampleValue = 91;

    [Test]
    public void GivenEquivalentAnnotatedTypeThenReturnZero()
    {
        // Arrange
        IComparable subject = new SimpleForInt(SampleValue);
        object value = new SimpleForInt(SampleValue);

        // Act
        int actual = subject.CompareTo(value);

        // Assert
        actual.ShouldBe(0);
    }

    [Test]
    public void GivenEquivalentEncapsulatedTypeThenReturnZero()
    {
        // Arrange
        IComparable subject = new SimpleForInt(SampleValue);
        object value = SampleValue;

        // Act
        int actual = subject.CompareTo(value);

        // Assert
        actual.ShouldBe(0);
    }

    [Test]
    public void GivenInvalidTypeThenThrowArgumentException()
    {
        // Arrange
        IComparable subject = new SimpleForInt(SampleValue);
        object value = string.Empty;

        // Act
        Action act = () => subject.CompareTo(value);

        // Assert
        act.ShouldThrow<ArgumentException>();
    }
}