namespace Monify.Console.Structs.Nested.InInterface.IOutterForIntTests;

public static class WhenConstructorIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenValueThenValueIsStored()
    {
        // Arrange
        IOutterForInt<int>.Inner instance = new(SampleValue);

        // Act
        int actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}
