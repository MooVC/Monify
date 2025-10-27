namespace Monify.Console.Structs.Nested.InStruct.OutterForStringTests.InnerTests;

public static class WhenConstructorIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueThenValueIsStored()
    {
        // Arrange
        OutterForString<int>.Inner instance = new(SampleValue);

        // Act
        string actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}