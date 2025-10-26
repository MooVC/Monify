namespace Monify.Console.Classes.Nested.InRecordStruct.OutterForStringTests.InnerTests;

public static class WhenImplicitOperatorFromStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        OutterForString<int>.Inner result = SampleValue;

        // Act
        string actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}