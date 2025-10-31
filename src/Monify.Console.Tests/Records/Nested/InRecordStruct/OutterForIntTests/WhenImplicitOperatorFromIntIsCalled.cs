namespace Monify.Console.Records.Nested.InRecordStruct.OutterForIntTests;

public static class WhenImplicitOperatorFromIntIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        OutterForInt<int>.Inner result = SampleValue;

        // Act
        int actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}