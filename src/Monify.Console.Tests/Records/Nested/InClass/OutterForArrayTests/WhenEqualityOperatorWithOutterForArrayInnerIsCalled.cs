namespace Monify.Console.Records.Nested.InClass.OutterForArrayTests;

public sealed class WhenEqualityOperatorWithOutterForArrayInnerIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenBothNullThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner? left = default;
        OutterForArray<int>.Inner? right = default;

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenDifferentValuesThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner left = new(_sampleValue);
        OutterForArray<int>.Inner right = new(_differentValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenEquivalentValuesThenReturnFalse()
    {
        // Arrange
        int[] leftValues = [1, 2, 3];
        int[] rightValues = [1, 2, 3];
        OutterForArray<int>.Inner left = new(leftValues);
        OutterForArray<int>.Inner right = new(rightValues);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenLeftIsNullThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner? left = default;
        OutterForArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner left = new(_sampleValue);
        OutterForArray<int>.Inner right = new(_sampleValue);

        // Act
        bool actual = left == right;

        // Assert
        actual.ShouldBeTrue();
    }
}