namespace Monify.Console.Classes.Nested.InStruct.OutterForStringTests;

public sealed class WhenEqualityOperatorWithStringIsCalled
{
    private const string SampleValue = "Sample";
    private const string DifferentValue = "Different";

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        OutterForString<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject == DifferentValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentValueThenReturnTrue()
    {
        // Arrange
        string subjectValue = new(SampleValue.ToCharArray());
        string comparisonValue = new(SampleValue.ToCharArray());
        OutterForString<int>.Inner subject = new(subjectValue);

        // Act
        bool actual = subject == comparisonValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForString<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject == SampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenSubjectIsNullThenReturnFalse()
    {
        // Arrange
        OutterForString<int>.Inner? subject = default;

        // Act
        bool actual = subject == SampleValue;

        // Assert
        actual.ShouldBeFalse();
    }
}