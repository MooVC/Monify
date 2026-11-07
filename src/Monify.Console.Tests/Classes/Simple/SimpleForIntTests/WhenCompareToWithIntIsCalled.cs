namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public sealed class WhenCompareToWithIntIsCalled
{
    private const int SampleValue = 91;

    [Test]
    public void GivenEquivalentValueThenReturnZero()
    {
        // Arrange
        IComparable<int> subject = new SimpleForInt(SampleValue);

        // Act
        int actual = subject.CompareTo(SampleValue);

        // Assert
        actual.ShouldBe(0);
    }
}