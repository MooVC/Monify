namespace Monify.Console.Classes.Nested.InInterface.IOutterForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenImplicitOperatorToImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenNullSubjectThenThrowsArgumentNullException()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner? subject = default;

        // Act
        Action act = () => _ = (ImmutableArray<string>)subject!;

        // Assert
        ArgumentNullException exception = Should.Throw<ArgumentNullException>(act);
        exception.ParamName.ShouldBe(nameof(subject));
    }

    [Test]
    public void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        IOutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        ImmutableArray<string> actual = subject;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}