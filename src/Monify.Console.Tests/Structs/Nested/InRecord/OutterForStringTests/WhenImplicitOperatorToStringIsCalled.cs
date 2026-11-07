namespace Monify.Console.Structs.Nested.InRecord.OutterForStringTests;

public sealed class WhenImplicitOperatorToStringIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        OutterForString<int>.Inner subject = new(SampleValue);

        // Act
        string actual = subject;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}