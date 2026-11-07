namespace Monify.Console.Structs.Nested.InRecord.OutterForIntTests;

public sealed class WhenEqualityOperatorWithOutterForIntInnerIsCalled
{
    private const int DifferentValue = 84;
    private const int SampleValue = 42;

    [Test]
    public void GivenBothNullThenReturnTrue()
    {
        // Arrange
        OutterForInt<int>.Inner? left = default;
        OutterForInt<int>.Inner? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        OutterForInt<int>.Inner left = new(SampleValue);
        OutterForInt<int>.Inner right = new(DifferentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        OutterForInt<int>.Inner? left = default;
        OutterForInt<int>.Inner right = new(SampleValue);

        // Act
        bool actual = left == right;

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
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }
}