namespace Monify.Console.Records.Nested.InClass.OutterForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenImplicitOperatorToImmutableArrayIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Fact]
    public static void GivenNullSubjectThenThrowsArgumentNullException()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner? subject = default;

        // Act
        Action act = () => _ = (ImmutableArray<string>)subject!;

        // Assert
        ArgumentNullException exception = Should.Throw<ArgumentNullException>(act);
        exception.ParamName.ShouldBe(nameof(subject));
    }

    [Fact]
    public static void GivenValidSubjectThenReturnsValue()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        ImmutableArray<string> actual = subject;

        // Assert
        actual.ShouldBe(_sampleValue);
    }
}