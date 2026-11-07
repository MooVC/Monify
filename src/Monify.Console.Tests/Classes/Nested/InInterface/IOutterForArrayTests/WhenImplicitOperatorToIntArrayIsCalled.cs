namespace Monify.Console.Classes.Nested.InInterface.IOutterForArrayTests;

public sealed class WhenImplicitOperatorToIntArrayIsCalled
{
    private static readonly int[] _sampleValue = [1, 2, 3];

    [Test]
    public void GivenNullSubjectThenThrowsArgumentNullException()
    {
        // Arrange
        IOutterForArray<int>.Inner? subject = default;

        // Act
        Action act = () => _ = (int[])subject!;

        // Assert
        ArgumentNullException exception = Should.Throw<ArgumentNullException>(act);
        exception.ParamName.ShouldBe(nameof(subject));
    }

    [Test]
    public void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        IOutterForArray<int>.Inner subject = new(_sampleValue);

        // Act
        int[] actual = subject;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}