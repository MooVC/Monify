namespace Monify.Console.Records.Nested.InInterface.IOutterForIntTests;

public sealed class WhenImplicitOperatorFromIntIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        IOutterForInt<int>.Inner result = SampleValue;

        // Act
        int actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}