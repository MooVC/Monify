namespace Monify.Console.Classes.Nested.InRecord.OutterForStringTests;

public sealed class WhenImplicitOperatorFromStringIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        OutterForString<int>.Inner result = SampleValue;

        // Act
        string actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}