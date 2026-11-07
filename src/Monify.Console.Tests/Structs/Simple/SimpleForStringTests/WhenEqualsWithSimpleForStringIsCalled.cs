namespace Monify.Console.Structs.Simple.SimpleForStringTests;

public sealed class WhenEqualsWithSimpleForStringIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        SimpleForString left = new(SampleValue);
        SimpleForString right = new(DifferentValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        SimpleForString left = new(SampleValue);
        SimpleForString right = new(SampleValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }
}