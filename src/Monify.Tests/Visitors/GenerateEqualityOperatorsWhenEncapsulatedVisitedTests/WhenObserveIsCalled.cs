namespace Monify.Visitors.GenerateEqualityOperatorsWhenEncapsulatedVisitedTests;

using Monify.Model;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenEncapsulatedTypesWithOperatorsThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues =
        [
            new Encapsulated { HasEqualityOperator = true, Type = "int" },
            new Encapsulated { HasEqualityOperator = true, Type = "string" },
        ];
        var visitor = new GenerateEqualityOperatorsWhenEncapsulatedVisited();

        // Act
        IEnumerable<Source> value = visitor.Observe(TestSubject.EncapsulatedAt(subject));
        IEnumerable<Source> passthrough = visitor.Observe(TestSubject.EncapsulatedAt(subject, 1));

        // Assert
        value.ShouldBeEmpty();
        passthrough.ShouldBeEmpty();
    }

    [Test]
    public void GivenEncapsulatedTypeWithoutOperatorThenValueIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var visitor = new GenerateEqualityOperatorsWhenEncapsulatedVisited();

        // Act
        Source value = visitor.Observe(TestSubject.EncapsulatedAt(subject)).Single();

        // Assert
        value.Hint.ShouldBe("Equality.Value");
    }

    [Test]
    public void GivenEncapsulatedPassthroughWithoutOperatorThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues = [new Encapsulated { Type = "int" }, new Encapsulated { Type = "string" }];
        var visitor = new GenerateEqualityOperatorsWhenEncapsulatedVisited();

        // Act
        Source source = visitor.Observe(TestSubject.EncapsulatedAt(subject, 1)).Single();

        // Assert
        source.Hint.ShouldBe("Equality.Passthrough.Level01");
    }
}