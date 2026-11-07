namespace Monify.Console.Records.Nested.InInterface.IOutterForStringTests;

public sealed class WhenImplicitOperatorFromStringIsCalled
{
    private const string SampleValue = "Sample";

    [Test]
    public void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        IOutterForString<int>.Inner result = SampleValue;

        // Act
        string actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}