namespace Monify.Console.Structs.Nested.InClass.OutterForStringTests;

public sealed class WhenEqualsWithStringIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        OutterForString<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject.Equals(DifferentValue);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForString<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject.Equals(SampleValue);

        // Assert
        actual.ShouldBeTrue();
    }
}