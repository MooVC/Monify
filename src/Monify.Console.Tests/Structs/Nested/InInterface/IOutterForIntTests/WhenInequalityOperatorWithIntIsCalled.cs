namespace Monify.Console.Structs.Nested.InInterface.IOutterForIntTests;

public sealed class WhenInequalityOperatorWithIntIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Test]
    public void GivenDifferentValueThenReturnTrue()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject != DifferentValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        bool actual = subject != SampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSubjectIsNullThenReturnTrue()
    {
        // Arrange
        IOutterForInt<int>.Inner? subject = default;

        // Act
        bool actual = subject != SampleValue;

        // Assert
        actual.ShouldBeTrue();
    }
}