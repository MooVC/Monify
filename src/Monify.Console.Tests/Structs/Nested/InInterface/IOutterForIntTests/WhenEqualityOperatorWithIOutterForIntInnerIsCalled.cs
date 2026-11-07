namespace Monify.Console.Structs.Nested.InInterface.IOutterForIntTests;

public sealed class WhenEqualityOperatorWithIOutterForIntInnerIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Test]
    public void GivenBothNullThenReturnTrue()
    {
        // Arrange
        IOutterForInt<int>.Inner? left = default;
        IOutterForInt<int>.Inner? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner left = new(SampleValue);
        IOutterForInt<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        IOutterForInt<int>.Inner? left = default;
        IOutterForInt<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

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
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }
}