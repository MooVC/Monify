namespace Monify.Console.Structs.Passthrough.OuterTests;

public static class WhenImplicitOperatorToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenNullSubjectThenNullIsReturned()
    {
        // Arrange
        Outer? subject = default;

        // Act
        string result = (string)subject!;

        // Assert
        result.ShouldBeNull();
    }

    [Fact]
    public static void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        Outer subject = new(SampleValue);

        // Act
        string actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}