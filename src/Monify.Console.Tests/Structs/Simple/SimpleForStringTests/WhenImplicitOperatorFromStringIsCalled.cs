namespace Monify.Console.Structs.Simple.SimpleForStringTests;

public sealed class WhenImplicitOperatorFromStringIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        SimpleForString result = SampleValue;

        // Act
        string actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}