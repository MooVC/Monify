namespace Monify.Console.Classes.Simple.SimpleForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenToStringIsCalled
{
    private static readonly string Expected = typeof(ImmutableArray<string>).ToString();
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        SimpleForImmutableArray subject = new(_sampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}