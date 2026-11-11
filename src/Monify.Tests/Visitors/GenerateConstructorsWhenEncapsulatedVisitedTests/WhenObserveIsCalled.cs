namespace Monify.Visitors.GenerateConstructorsWhenEncapsulatedVisitedTests;

using Monify.Model;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenPassthroughEncapsulatedValueThenHintIncludesPassthrough()
    {
        // Arrange
        Subject subject = TestSubject.Create();

        subject.EncapsulatedValues =
        [
            new Encapsulated { HasConstructor = true, Type = "int" },
            new Encapsulated { Type = "string" },
        ];

        var visitor = new GenerateConstructorsWhenEncapsulatedVisited();

        // Act
        Source source = visitor.Observe(TestSubject.EncapsulatedAt(subject, 1)).Single();

        // Assert
        source.Hint.ShouldBe(".ctor.Passthrough.01");
        source.Code.ShouldContain("public Sample(string value)");
    }

    [Test]
    public void GivenSubjectWhenHasConstructorThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues[0].HasConstructor = true;
        var visitor = new GenerateConstructorsWhenEncapsulatedVisited();

        // Act
        IEnumerable<Source> result = visitor.Observe(TestSubject.EncapsulatedAt(subject));

        // Assert
        result.ShouldBeEmpty();
    }

    [Test]
    [Arguments("int")]
    [Arguments("global::Example.PayloadAttribute")]
    [Arguments("global::Example.Outer<int>.Inner<global::Example.PayloadAttribute>[,][]")]
    public void GivenSubjectWhenLacksConstructorThenSourceIsReturned(string valueType)
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues[0].Type = valueType;
        var visitor = new GenerateConstructorsWhenEncapsulatedVisited();

        // Act
        Source source = visitor.Observe(TestSubject.EncapsulatedAt(subject)).Single();

        // Assert
        source.Hint.ShouldBe(GenerateConstructorsWhenEncapsulatedVisited.Name);
        source.Code.ShouldContain($"public Sample({valueType} value)");
    }
}