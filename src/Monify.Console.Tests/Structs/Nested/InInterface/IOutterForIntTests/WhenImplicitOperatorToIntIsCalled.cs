namespace Monify.Console.Structs.Nested.InInterface.IOutterForIntTests;

public sealed class WhenImplicitOperatorToIntIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        IOutterForInt<int>.Inner subject = new(SampleValue);

        // Act
        int actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}