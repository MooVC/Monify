namespace Monify.Console.Records.Nested.InStruct.OutterForImmutableArrayTests;

using System.Collections.Immutable;

public static class WhenToStringIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Fact]
    public static void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("Inner { }");
    }
}