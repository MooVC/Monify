namespace Monify.Console.Structs.Nested.InClass.OutterForStringTests.InnerTests;

public static class WhenImplicitOperatorToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Fact]
    public static void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        OutterForString<int>.Inner subject = new(SampleValue);

        // Act
        string actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}