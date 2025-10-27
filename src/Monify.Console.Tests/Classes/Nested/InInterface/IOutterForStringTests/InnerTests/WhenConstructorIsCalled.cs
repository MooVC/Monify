namespace Monify.Console.Classes.Nested.InInterface.IOutterForStringTests.InnerTests;

public static class WhenConstructorIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueThenValueIsStored()
    {
        // Arrange
        IOutterForString<int>.Inner instance = new(SampleValue);

        // Act
        string actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}