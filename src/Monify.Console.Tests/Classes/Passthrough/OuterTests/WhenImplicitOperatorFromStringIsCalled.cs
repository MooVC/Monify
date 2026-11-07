namespace Monify.Console.Classes.Passthrough.OuterTests;

public sealed class WhenImplicitOperatorFromStringIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        Outer result = SampleValue;

        // Act
        string actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}