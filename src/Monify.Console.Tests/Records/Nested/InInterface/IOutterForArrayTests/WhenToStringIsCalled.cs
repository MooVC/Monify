namespace Monify.Console.Records.Nested.InInterface.IOutterForArrayTests;

public sealed class WhenToStringIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        IOutterForArray<int>.Inner subject = new(_sampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("Inner { }");
    }
}