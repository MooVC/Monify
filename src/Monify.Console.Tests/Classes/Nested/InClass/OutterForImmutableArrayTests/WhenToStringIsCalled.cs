namespace Monify.Console.Classes.Nested.InClass.OutterForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenToStringIsCalled
{
    private static readonly string Expected = $"Inner {{ {typeof(ImmutableArray<string>)} }}";
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Fact]
    public static void GivenValueTheExpectedStringIsReturned()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        string result = subject.ToString();

        // Assert
        result.ShouldBe(Expected);
    }
}