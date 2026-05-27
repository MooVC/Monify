namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public static class WhenCompareToWithIntIsCalled
{
    private const int SampleValue = 91;

    [Fact]
    public static void GivenEquivalentValueThenReturnZero()
    {
        // Arrange
        IComparable<int> subject = new SimpleForInt(SampleValue);

        // Act
        int actual = subject.CompareTo(SampleValue);

        // Assert
        actual.ShouldBe(0);
    }
}