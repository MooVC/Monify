namespace Monify.Console.Structs.Nested.InClass.OutterForIntTests;

public static class WhenImplicitOperatorToIntIsCalled
{
    private const int SampleValue = 42;

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
