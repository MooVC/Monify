namespace Monify.Visitors.GeneratePassthroughMethodWhenPassthroughMethodVisitedTests;

using Monify.Model;
using MethodNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Methods.PassthroughMethod;
using MethodsNode = Monify.Model.Subject.Graph.EncapsulatedValues.Encapsulated.Methods;

public sealed class WhenObserveIsCalled
{
    [Test]
    public void GivenMethodsThenSourcesAreGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues =
        [
            new Encapsulated
            {
                Methods =
                [
                    new PassthroughMethod
                    {
                        Accessibility = "public",
                        Name = "Format",
                        Parameters =
                        [
                            new PassthroughParameter
                            {
                                Name = "value",
                                Type = "int",
                            },
                        ],
                        Return = "string",
                    },
                    new PassthroughMethod
                    {
                        ExplicitInterface = "global::Sample.IValue",
                        Name = "Reset",
                        Return = "void",
                    },
                ],
                Type = "global::Sample.Value",
            },
        ];
        var visitor = new GeneratePassthroughMethodWhenPassthroughMethodVisited();

        // Act
        Source format = visitor.Observe(MethodAt(subject)).Single();
        Source reset = visitor.Observe(MethodAt(subject, index: 1)).Single();

        // Assert
        format.Hint.ShouldBe("Methods.Format.int");
        format.Code.ShouldContain("public string Format(int value)");
        format.Code.ShouldContain("return _value.Format(value);");
        reset.Hint.ShouldBe("Methods.globalSampleIValue.Reset");
        reset.Code.ShouldContain("void global::Sample.IValue.Reset()");
        reset.Code.ShouldContain("((global::Sample.IValue)_value).Reset();");
    }

    [Test]
    public void GivenMethodWithObjectParameterThenAnnotatedValueIsUnwrapped()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues =
        [
            new Encapsulated
            {
                Methods =
                [
                    new PassthroughMethod
                    {
                        Accessibility = "public",
                        Name = "CompareTo",
                        Parameters =
                        [
                            new PassthroughParameter
                            {
                                Name = "value",
                                Type = "object",
                            },
                        ],
                        Return = "int",
                    },
                ],
                Type = "global::Sample.Value",
            },
        ];
        var visitor = new GeneratePassthroughMethodWhenPassthroughMethodVisited();

        // Act
        Source source = visitor.Observe(MethodAt(subject)).Single();

        // Assert
        source.Code.ShouldContain("if (value is Sample)");
        source.Code.ShouldContain("value = ((Sample)value)._value;");
        source.Code.ShouldContain("return _value.CompareTo(value);");
    }

    [Test]
    public void GivenPassthroughLevelMethodThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.EncapsulatedValues =
        [
            new Encapsulated { Type = "global::Sample.Inner" },
            new Encapsulated
            {
                Methods =
                [
                    new PassthroughMethod
                    {
                        Accessibility = "public",
                        Name = "Format",
                        Return = "string",
                    },
                ],
                Type = "global::Sample.Value",
            },
        ];
        var visitor = new GeneratePassthroughMethodWhenPassthroughMethodVisited();

        // Act
        IEnumerable<Source> result = visitor.Observe(MethodAt(subject, level: 1));

        // Assert
        result.ShouldBeEmpty();
    }

    private static MethodNode MethodAt(Subject subject, int index = 0, int level = 0)
    {
        var encapsulated = TestSubject.EncapsulatedAt(subject, level);
        var methods = new MethodsNode(encapsulated, subject, encapsulated._Value.Methods);

        return new MethodNode(methods, index, subject, methods._Value[index]);
    }
}