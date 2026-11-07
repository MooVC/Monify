namespace Monify.Console.Structs.Nested.InInterface.IOutterForStringTests;

public sealed class WhenImplicitOperatorToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        IOutterForString<int>.Inner subject = new(SampleValue);

        // Act
        string actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}