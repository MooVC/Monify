namespace Monify.Console.Structs.Passthrough.InnerTests;

public static class WhenImplicitOperatorToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenNullSubjectThenNullIsReturned()
    {
        // Arrange
        Inner? subject = default;

        // Act
        string result = (string)subject!;

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public static void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        Inner subject = new(SampleValue);

        // Act
        string actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}