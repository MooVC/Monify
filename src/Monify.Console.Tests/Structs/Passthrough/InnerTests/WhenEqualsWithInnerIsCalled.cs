namespace Monify.Console.Structs.Passthrough.InnerTests;

public sealed class WhenEqualsWithInnerIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        Inner left = new(SampleValue);
        Inner right = new(DifferentValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        Inner left = new(SampleValue);
        Inner right = new(SampleValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }
}