namespace Monify.Visitors.GeneratePassthroughPropertyWhenPassthroughPropertyVisitedTests;

using Monify.Model;
using PropertiesNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Properties;
using PropertyNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Properties.PassthroughProperty;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenPropertiesThenSourcesAreGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues =
        [
            new Encapsulated
            {
                Properties =
                [
                    new PassthroughProperty
                    {
                        Accessibility = "public",
                        HasGetter = true,
                        HasSetter = true,
                        Name = "Name",
                        Type = "string",
                    },
                    new PassthroughProperty
                    {
                        ExplicitInterface = "global::Sample.IValue",
                        HasGetter = true,
                        Name = "Count",
                        Type = "int",
                    },
                ],
                Type = "global::Sample.Value",
            },
        ];
        var visitor = new GeneratePassthroughPropertyWhenPassthroughPropertyVisited();

        // Act
        Source name = visitor.Observe(PropertyAt(subject)).Single();
        Source count = visitor.Observe(PropertyAt(subject, index: 1)).Single();

        // Assert
        name.Hint.ShouldBe("Properties.Name");
        name.Code.ShouldContain("public string Name");
        name.Code.ShouldContain("return _value.Name;");
        name.Code.ShouldContain("_value.Name = value;");
        count.Hint.ShouldBe("Properties.globalSampleIValue.Count");
        count.Code.ShouldContain("int global::Sample.IValue.Count");
        count.Code.ShouldContain("return ((global::Sample.IValue)_value).Count;");
    }

    [Test]
    public void GivenPassthroughLevelPropertyThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues =
        [
            new Encapsulated { Type = "global::Sample.Inner" },
            new Encapsulated
            {
                Properties =
                [
                    new PassthroughProperty
                    {
                        Accessibility = "public",
                        HasGetter = true,
                        Name = "Name",
                        Type = "string",
                    },
                ],
                Type = "global::Sample.Value",
            },
        ];
        var visitor = new GeneratePassthroughPropertyWhenPassthroughPropertyVisited();

        // Act
        IEnumerable<Source> result = visitor.Observe(PropertyAt(subject, level: 1));

        // Assert
        result.ShouldBeEmpty();
    }

    private static PropertyNode PropertyAt(Subject subject, int index = 0, int level = 0)
    {
        var encapsulated = TestSubject.EncapsulatedAt(subject, level);
        var properties = new PropertiesNode(encapsulated, subject, encapsulated._Value.Properties);

        return new PropertyNode(properties, index, subject, properties._Value[index]);
    }
}