namespace Monify.Console.Classes.Nested.InRecord.OutterForArrayTests;

public sealed class WhenEqualityOperatorWithIntArrayIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject == _differentValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject == _sampleValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenSubjectIsNullThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner? subject = default;

        // Act
        bool actual = subject == _sampleValue;

        // Assert
        actual.ShouldBeFalse();
    }
}