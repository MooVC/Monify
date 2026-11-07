namespace Monify.Console.Structs.Nested.InClass.OutterForIntTests;

public sealed class WhenInequalityOperatorWithOutterForIntInnerIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Test]
    public void GivenDifferentValuesThenReturnTrue()
    {
        // Arrange
        OutterForInt<int>.Inner left = new(SampleValue);
        OutterForInt<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        OutterForInt<int>.Inner left = new(SampleValue);
        OutterForInt<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left != right;

        // Assert
        actual.ShouldBeFalse();
    }
}