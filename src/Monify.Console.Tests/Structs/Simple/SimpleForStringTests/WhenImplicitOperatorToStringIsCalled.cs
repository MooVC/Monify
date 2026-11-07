namespace Monify.Console.Structs.Simple.SimpleForStringTests;

public sealed class WhenImplicitOperatorToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);

        // Act
        string actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}