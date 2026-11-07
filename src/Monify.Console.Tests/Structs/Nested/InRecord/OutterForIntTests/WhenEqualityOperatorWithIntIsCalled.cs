namespace Monify.Console.Structs.Nested.InRecord.OutterForIntTests;

public sealed class WhenEqualityOperatorWithIntIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject == DifferentValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject == SampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenSubjectIsNullThenReturnFalse()
    {
        // Arrange
        OutterForInt<int>.Inner? subject = default;

        // Act
        bool actual = subject == SampleValue;

        // Assert
        actual.ShouldBeFalse();
    }
}