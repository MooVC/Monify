namespace Monify.Console.Records.Nested.InInterface.IOutterForStringTests;

public sealed class WhenEqualsWithIOutterForStringInnerIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        IOutterForString<int>.Inner left = new(SampleValue);
        IOutterForString<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        IOutterForString<int>.Inner left = new(SampleValue);
        IOutterForString<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }
}