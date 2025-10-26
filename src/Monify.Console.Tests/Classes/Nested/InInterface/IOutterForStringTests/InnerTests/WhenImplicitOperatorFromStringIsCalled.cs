namespace Monify.Console.Classes.Nested.InInterface.IOutterForStringTests.InnerTests;

public static class WhenImplicitOperatorFromStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        IOutterForString<int>.Inner result = SampleValue;

        // Act
        string actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}