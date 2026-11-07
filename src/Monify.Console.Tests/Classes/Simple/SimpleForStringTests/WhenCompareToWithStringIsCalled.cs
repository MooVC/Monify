namespace Monify.Console.Classes.Simple.SimpleForStringTests;

public sealed class WhenCompareToWithStringIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenEquivalentValueThenReturnZero()
    {
        // Arrange
        IComparable<string> subject = new SimpleForString(SampleValue);

        // Act
        int actual = subject.CompareTo(SampleValue);

        // Assert
        actual.ShouldBe(0);
    }
}