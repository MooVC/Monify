namespace Monify.Console.Structs.Nested.InStruct.OutterForStringTests;

public sealed class WhenConstructorIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValueThenValueIsStored()
    {
        // Arrange
        OutterForString<int>.Inner instance = new(SampleValue);

        // Act
        string actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}