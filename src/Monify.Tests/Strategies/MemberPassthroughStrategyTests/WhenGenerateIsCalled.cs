namespace Monify.Strategies.MemberPassthroughStrategyTests;

using Monify.Model;

public sealed class WhenGenerateIsCalled
{
    [Fact]
    public void GivenMethodsThenSourcesAreGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.Encapsulated =
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
        var strategy = new MemberPassthroughStrategy();

        // Act
        Source[] sources = strategy.Generate(subject).ToArray();

        // Assert
        sources.Length.ShouldBe(2);
        sources[0].Hint.ShouldBe("Methods.Format.int");
        sources[0].Code.ShouldContain("public string Format(int value)");
        sources[0].Code.ShouldContain("return _value.Format(value);");
        sources[1].Hint.ShouldBe("Methods.globalSampleIValue.Reset");
        sources[1].Code.ShouldContain("void global::Sample.IValue.Reset()");
        sources[1].Code.ShouldContain("((global::Sample.IValue)_value).Reset();");
    }

    [Fact]
    public void GivenMethodWithObjectParameterThenAnnotatedValueIsUnwrapped()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.Encapsulated =
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
        var strategy = new MemberPassthroughStrategy();

        // Act
        Source source = strategy.Generate(subject).Single();

        // Assert
        source.Code.ShouldContain("if (value is Sample)");
        source.Code.ShouldContain("value = ((Sample)value)._value;");
        source.Code.ShouldContain("return _value.CompareTo(value);");
    }

    [Fact]
    public void GivenPassthroughLevelMembersThenNoSourceIsGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.Encapsulated =
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
        var strategy = new MemberPassthroughStrategy();

        // Act
        IEnumerable<Source> result = strategy.Generate(subject);

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void GivenPropertiesThenSourcesAreGenerated()
    {
        // Arrange
        Subject subject = TestSubject.Create();
        subject.Encapsulated =
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
        var strategy = new MemberPassthroughStrategy();

        // Act
        Source[] sources = strategy.Generate(subject).ToArray();

        // Assert
        sources.Length.ShouldBe(2);
        sources[0].Hint.ShouldBe("Properties.Name");
        sources[0].Code.ShouldContain("public string Name");
        sources[0].Code.ShouldContain("return _value.Name;");
        sources[0].Code.ShouldContain("_value.Name = value;");
        sources[1].Hint.ShouldBe("Properties.globalSampleIValue.Count");
        sources[1].Code.ShouldContain("int global::Sample.IValue.Count");
        sources[1].Code.ShouldContain("return ((global::Sample.IValue)_value).Count;");
    }
}