namespace Monify.Console.Structs.Nested.InRecordStruct.OutterForArrayTests;

public sealed class WhenEqualsWithIntArrayIsCalled
{
    private static readonly int[] _differentValue = [4, 5, 6];
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenDifferentValueThenReturnFalse()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject.Equals(_differentValue);

        // Assert
        actual.ShouldBeFalse();
    }

    [Test]
    public void GivenSameValueThenReturnTrue()
    {
        // Arrange
        OutterForArray<int>.Inner subject = new(_sampleValue);

        // Act
        bool actual = subject.Equals(_sampleValue);

        // Assert
        actual.ShouldBeTrue();
    }
}