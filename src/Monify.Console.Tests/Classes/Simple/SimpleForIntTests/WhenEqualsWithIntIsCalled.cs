namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public sealed class WhenEqualsWithIntIsCalled
{
    private const int DifferentValue = 12;
    private const int SampleValue = 36;

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        bool actual = subject.Equals(DifferentValue);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForInt subject = new(SampleValue);

        // Act
        bool actual = subject.Equals(SampleValue);

        // Assert
        actual.ShouldBeTrue();
    }
}