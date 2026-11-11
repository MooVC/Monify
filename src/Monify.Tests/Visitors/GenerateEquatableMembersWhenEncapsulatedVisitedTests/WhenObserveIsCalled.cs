namespace Monify.Visitors.GenerateEquatableMembersWhenEncapsulatedVisitedTests;

using Monify.Model;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenPassthroughEncapsulatedSequenceThenSequenceComparerIsUsed()
    {
        // Arrange
        var visitor = new GenerateEquatableMembersWhenEncapsulatedVisited();
        Subject subject = TestSubject.Create();

        subject.EncapsulatedValues =
        [
            new Encapsulated { Type = "int" },
            new Encapsulated
            {
                HasEquatable = false,
                IsEquatable = true,
                IsSequence = true,
                Type = "global::System.Collections.Generic.IEnumerable<int>",
            },
        ];

        // Act
        Source source = visitor.Observe(TestSubject.EncapsulatedAt(subject, 1)).Single();

        // Assert
        source.Hint.ShouldBe("IEquatable.Passthrough.01.Equals");
        source.Code.ShouldContain("SequenceEqualityComparer.Default.Equals");
    }

    [Test]
    public void GivenSequenceEncapsulatedValueThenImmutableArrayCheckIsGenerated()
    {
        // Arrange
        var visitor = new GenerateEquatableMembersWhenEncapsulatedVisited();
        Subject subject = TestSubject.Create();

        subject.EncapsulatedValues =
        [
            new Encapsulated
            {
                HasEquatable = false,
                IsEquatable = true,
                IsSequence = true,
                Type = "global::System.Collections.Immutable.ImmutableArray<int>",
            },
        ];

        // Act
        Source source = visitor.Observe(TestSubject.EncapsulatedAt(subject)).Single();

        // Assert
        source.Hint.ShouldBe("IEquatable.Value.Equals");
        source.Code.ShouldContain("IsDefault ? other.IsDefault");
    }
}