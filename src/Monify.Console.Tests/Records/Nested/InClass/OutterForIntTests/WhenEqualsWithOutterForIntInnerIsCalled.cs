namespace Monify.Console.Records.Nested.InClass.OutterForIntTests;

public sealed class WhenEqualsWithOutterForIntInnerIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        OutterForInt<int>.Inner left = new(SampleValue);
        OutterForInt<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForInt<int>.Inner left = new(SampleValue);
        OutterForInt<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left.Equals(right);

        // Assert
        actual.ShouldBeTrue();
    }
}