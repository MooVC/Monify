namespace Monify.Visitors.GenerateInequalityOperatorsWhenEncapsulatedVisitedTests;

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
            new Encapsulated { HasInequalityOperator = true, Type = "int" },
            new Encapsulated { HasInequalityOperator = true, Type = "string" },
        ];
        var visitor = new GenerateInequalityOperatorsWhenEncapsulatedVisited();

        // Act
        IEnumerable<Source> value = visitor.Observe(TestSubject.EncapsulatedAt(subject));
        IEnumerable<Source> passthrough = visitor.Observe(TestSubject.EncapsulatedAt(subject, 1));

        // Assert
        value.ShouldBeEmpty();
        passthrough.ShouldBeEmpty();
    }

    [Test]
    public void GivenEncapsulatedValueWithoutOperatorThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        var visitor = new GenerateInequalityOperatorsWhenEncapsulatedVisited();

        // Act
        Source value = visitor.Observe(TestSubject.EncapsulatedAt(subject)).Single();

        // Assert
        value.Hint.ShouldBe("Inequality.Value");
    }

    [Test]
    public void GivenEncapsulatedPassthroughWithoutOperatorThenSourceIsReturned()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues = [new Encapsulated { Type = "int" }, new Encapsulated { Type = "string" }];
        var visitor = new GenerateInequalityOperatorsWhenEncapsulatedVisited();

        // Act
        Source source = visitor.Observe(TestSubject.EncapsulatedAt(subject, 1)).Single();

        // Assert
        source.Hint.ShouldBe("Inequality.Passthrough.Level01");
    }
}