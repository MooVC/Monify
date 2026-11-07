namespace Monify.Console.Records.Nested.InClass.OutterForArrayTests;

public sealed class WhenInequalityOperatorWithIntArrayIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenDifferentValueThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject != _differentValue;

        // Assert
        actual.ShouldBeTrue();
    }

    [Test]
    public void GivenSameValueThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject != _sampleValue;

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSubjectIsNullThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner? subject = default;

        // Act
        bool actual = subject != _sampleValue;

        // Assert
        actual.ShouldBeTrue();
    }
}