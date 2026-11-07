namespace Monify.Console.Structs.Nested.InInterface.IOutterForStringTests;

public sealed class WhenInequalityOperatorWithStringIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenDifferentValueThenReturnTrue()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject != DifferentValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenEquivalentValueThenReturnFalse()
    {
        // Arrange
        string subjectValue = new(SampleValue.ToCharArray());
        string comparisonValue = new(SampleValue.ToCharArray());
        IOutterForString<int>.Inner subject = new(subjectValue);

        // Act
        bool actual = subject != comparisonValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject != SampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSubjectIsNullThenReturnTrue()
    {
        // Arrange
        IOutterForString<int>.Inner? subject = default;

        // Act
        bool actual = subject != SampleValue;

        // Assert
        actual.ShouldBeTrue();
    }
}