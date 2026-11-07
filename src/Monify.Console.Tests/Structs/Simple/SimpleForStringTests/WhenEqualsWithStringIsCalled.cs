namespace Monify.Console.Structs.Simple.SimpleForStringTests;

public sealed class WhenEqualsWithStringIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);

        // Act
        bool actual = subject.Equals(DifferentValue);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);

        // Act
        bool actual = subject.Equals(SampleValue);

        // Assert
        actual.ShouldBeTrue();
    }
}