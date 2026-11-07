namespace Monify.Console.Structs.Simple.SimpleForIntTests;

public sealed class WhenImplicitOperatorFromIntIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        SimpleForInt result = SampleValue;

        // Act
        int actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}