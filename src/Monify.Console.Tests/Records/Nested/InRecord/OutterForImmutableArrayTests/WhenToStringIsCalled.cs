namespace Monify.Console.Records.Nested.InRecord.OutterForImmutableArrayTests;

using System.Collections.Immutable;

public sealed class WhenToStringIsCalled
{
    private static readonly ImmutableArray<string> _sampleValue = ["Alpha", "Beta", "Gamma"];

    [Test]
    public void GivenValueThenReturnRecordDescription()
    {
        // Arrange
        OutterForImmutableArray<int>.Inner subject = new(_sampleValue);

        // Act
        string actual = subject.ToString();

        // Assert
        actual.ShouldBe("Inner { IsDefault = False, IsDefaultOrEmpty = False, IsEmpty = False, Length = 3 }");
    }
}