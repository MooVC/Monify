namespace Monify.Console.Structs.Simple.SimpleForStringTests;

public static class WhenImplicitOperatorToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        SimpleForString subject = new(SampleValue);

        // Act
        string actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}