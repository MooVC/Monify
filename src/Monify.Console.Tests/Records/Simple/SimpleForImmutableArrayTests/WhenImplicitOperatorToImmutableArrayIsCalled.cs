namespace Monify.Console.Records.Simple.SimpleForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenImplicitOperatorToImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenNullSubjectThenThrowsArgumentNullException()
    {
        // Arrange
        SimpleForImmutableArray? subject = default;

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
        SimpleForImmutableArray subject = new(_sampleValue);

        // Act
        ImmutableArray<string> actual = subject;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}