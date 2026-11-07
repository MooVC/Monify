namespace Monify.Console.Classes.Simple.SimpleForIntTests;

public sealed class WhenImplicitOperatorFromIntIsCalled
{
    private const int SampleValue = 77;

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