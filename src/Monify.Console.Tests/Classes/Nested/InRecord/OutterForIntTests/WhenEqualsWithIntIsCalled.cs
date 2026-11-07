namespace Monify.Console.Classes.Nested.InRecord.OutterForIntTests;

public sealed class WhenEqualsWithIntIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject.Equals(DifferentValue);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject.Equals(SampleValue);

        // Assert
        actual.ShouldBeTrue();
    }
}