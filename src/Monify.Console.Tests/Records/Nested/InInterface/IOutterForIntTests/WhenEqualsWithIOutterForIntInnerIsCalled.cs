namespace Monify.Console.Records.Nested.InInterface.IOutterForIntTests;

public sealed class WhenEqualsWithIOutterForIntInnerIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner left = new(SampleValue);
        IOutterForInt<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        IOutterForInt<int>.Inner left = new(SampleValue);
        IOutterForInt<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }
}