namespace Monify.Console.Classes.Simple.SimpleForIntTests;

using Shouldly;
using Xunit;

public static class WhenImplicitOperatorFromIntIsCalled
{
    private const int SampleValue = 77;

    [Fact]
    public static void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange & Act
        SimpleForInt result = SampleValue;

        // Assert
        int actual = result;
        actual.ShouldBe(SampleValue);
    }
}