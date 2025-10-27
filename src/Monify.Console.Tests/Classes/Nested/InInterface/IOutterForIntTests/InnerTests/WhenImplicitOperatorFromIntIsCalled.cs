namespace Monify.Console.Classes.Nested.InInterface.IOutterForIntTests.InnerTests;

public static class WhenImplicitOperatorFromIntIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        IOutterForInt<int>.Inner result = SampleValue;

        // Act
        int actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}