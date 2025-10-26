namespace Monify.Console.Records.Nested.InRecord.OutterForIntTests.InnerTests;

public static class WhenImplicitOperatorToIntIsCalled
{
    private const int SampleValue = 42;

    [Fact]
    public static void GivenNullSubjectThenThrowsArgumentNullException()
    {
        // Arrange
        OutterForInt<int>.Inner? subject = default;

        // Act
        Action act = () => _ = (int)subject!;

        // Assert
        ArgumentNullException exception = Should.Throw<ArgumentNullException>(act);
        exception.ParamName.ShouldBe("subject");
    }

    [Fact]
    public static void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        int actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}