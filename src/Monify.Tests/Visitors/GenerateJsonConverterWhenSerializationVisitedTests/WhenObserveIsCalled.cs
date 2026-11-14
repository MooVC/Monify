namespace Monify.Visitors.GenerateJsonConverterWhenSerializationVisitedTests;

using Monify.Model;

public sealed class WhenObserveIsCalled
{
    [Test]
    [Arguments("sealed partial class")]
    [Arguments("sealed partial record")]
    [Arguments("partial struct")]
    [Arguments("readonly partial struct")]
    [Arguments("readonly partial record struct")]
    public void GivenSubjectWithoutJsonConverterThenNestedConverterIsGenerated(string declaration)
    {
        // Arrange
        var subject = TestSubject.Create();
        subject.Declaration = declaration;
        subject.QualifiedName = "global::Sample";
        subject.Serialization = subject.Serialization.SupportsJsonSerialization(true);
        var visitor = new GenerateJsonConverterWhenSerializationVisited();

        // Act
        var source = visitor.Observe(new Subject.Graph.Serialization(subject, subject.Serialization)).Single();

        // Assert
        source.Hint.ShouldBe("JsonConverter");
        source.Code.ShouldContain("[global::System.Text.Json.Serialization.JsonConverter(typeof(global::Sample.Converter))]");
        source.Code.ShouldContain("public sealed class Converter\n        : global::System.Text.Json.Serialization.JsonConverter<global::Sample>");
        source.Code.ShouldContain("return new global::Sample(global::System.Text.Json.JsonSerializer.Deserialize<int>(ref reader, options));");
        source.Code.ShouldContain("global::System.Text.Json.JsonSerializer.Serialize<int>(writer, value._value, options);");
    }

    [Test]
    public void GivenSubjectWithJsonConverterThenNoSourceIsGenerated()
    {
        // Arrange
        var subject = TestSubject.Create();
        subject.Serialization = subject.Serialization
            .HasJsonConverter(true)
            .SupportsJsonSerialization(true);
        var visitor = new GenerateJsonConverterWhenSerializationVisited();

        // Act
        var sources = visitor.Observe(new Subject.Graph.Serialization(subject, subject.Serialization));

        // Assert
        sources.ShouldBeEmpty();
    }

    [Test]
    public void GivenCompilationWithoutJsonSerializationThenNoSourceIsGenerated()
    {
        // Arrange
        var subject = TestSubject.Create();
        var visitor = new GenerateJsonConverterWhenSerializationVisited();

        // Act
        var sources = visitor.Observe(new Subject.Graph.Serialization(subject, subject.Serialization));

        // Assert
        sources.ShouldBeEmpty();
    }

    [Test]
    [Arguments("", "global::Sample_1JsonConverterFactory", "global::Sample<T>")]
    [Arguments("Example", "global::Example.Sample_1JsonConverterFactory", "global::Example.Sample<T>")]
    public void GivenGenericSubjectThenFactoryAndNestedConverterAreGenerated(string namespaceName, string converterType, string subjectType)
    {
        // Arrange
        var subject = TestSubject.Create();
        subject.Serialization = subject.Serialization
            .WithJsonConverterFactoryName("Sample_1JsonConverterFactory")
            .WithJsonConverterFactoryTarget("Sample`1")
            .SupportsJsonSerialization(true);
        subject.Namespace = namespaceName;
        subject.Qualification = "Sample<T>";
        subject.QualifiedName = subjectType;
        var visitor = new GenerateJsonConverterWhenSerializationVisited();

        // Act
        var sources = visitor.Observe(new Subject.Graph.Serialization(subject, subject.Serialization)).ToArray();

        // Assert
        sources.Length.ShouldBe(2);
        sources[0].Hint.ShouldBe("JsonConverterFactory");
        sources[0].IsNested.ShouldBeFalse();
        sources[0].Code.ShouldContain("typeToConvert.GetGenericTypeDefinition().FullName == \"Sample`1\"");
        sources[1].Code.ShouldContain($"typeof({converterType})");
        sources[1].Code.ShouldContain($"JsonConverter<{subjectType}>");
    }
}