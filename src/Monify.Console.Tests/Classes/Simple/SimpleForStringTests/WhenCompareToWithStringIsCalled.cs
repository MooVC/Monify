namespace Monify.Console.Classes.Simple.SimpleForStringTests;

public static class WhenCompareToWithStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenEquivalentValueThenReturnZero()
    {
        // Arrange
        IComparable<string> subject = new SimpleForString(SampleValue);

        // Act
        int actual = subject.CompareTo(SampleValue);

        // Assert
        actual.ShouldBe(0);
    }
}