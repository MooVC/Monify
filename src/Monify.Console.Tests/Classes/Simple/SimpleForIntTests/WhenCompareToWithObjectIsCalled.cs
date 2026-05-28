namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public static class WhenCompareToWithObjectIsCalled
{
    private const int SampleValue = 91;

    [Fact]
    public static void GivenEquivalentAnnotatedTypeThenReturnZero()
    {
        // Arrange
        IComparable subject = new SimpleForInt(SampleValue);
        object value = new SimpleForInt(SampleValue);

        // Act
        int actual = subject.CompareTo(value);

        // Assert
        actual.ShouldBe(0);
    }

    [Fact]
    public static void GivenEquivalentEncapsulatedTypeThenReturnZero()
    {
        // Arrange
        IComparable subject = new SimpleForInt(SampleValue);
        object value = SampleValue;

        // Act
        int actual = subject.CompareTo(value);

        // Assert
        actual.ShouldBe(0);
    }

    [Fact]
    public static void GivenInvalidTypeThenThrowArgumentException()
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