namespace Monify.Console.Records.Nested.InInterface.IOutterForArrayTests;

public sealed class WhenConstructorIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenValueThenValueIsStored()
    {
        // Arrange
        IOutterForArray<int>.Inner instance = new(_sampleValue);

        // Act
        int[] actual = instance;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}