namespace Monify.Console.Records.Nested.InStruct.OutterForIntTests.InnerTests;

public static class WhenConstructorIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenValueThenValueIsStored()
    {
        // Arrange
        OutterForInt<int>.Inner instance = new(SampleValue);

        // Act
        int actual = instance;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}