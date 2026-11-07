namespace Monify.Console.Structs.Nested.InClass.OutterForIntTests;

public sealed class WhenImplicitOperatorToIntIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        OutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        int actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}