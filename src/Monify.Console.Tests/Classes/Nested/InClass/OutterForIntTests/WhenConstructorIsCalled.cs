namespace Monify.Console.Classes.Nested.InClass.OutterForIntTests;

public sealed class WhenConstructorIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenValueThenValueIsStored()
    {
        // Arrange
        OutterForInt<int>.Inner instance = new(SampleValue);

        // Act
        int actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}