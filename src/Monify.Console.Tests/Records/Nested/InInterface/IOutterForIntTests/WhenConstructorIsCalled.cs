namespace Monify.Console.Records.Nested.InInterface.IOutterForIntTests;

public sealed class WhenConstructorIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenValueThenValueIsStored()
    {
        // Arrange
        IOutterForInt<int>.Inner instance = new(SampleValue);

        // Act
        int actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}