namespace Monify.Console.Structs.Nested.InClass.OutterForIntTests;

public sealed class WhenImplicitOperatorFromIntIsCalled
{
    private const int SampleValue = 42;

    [Test]
    public void GivenValueThenReturnsEquivalentInstance()
    {
        // Arrange
        OutterForInt<int>.Inner result = SampleValue;

        // Act
        int actual = result;

        // Assert
        actual.ShouldBe(SampleValue);
    }
}