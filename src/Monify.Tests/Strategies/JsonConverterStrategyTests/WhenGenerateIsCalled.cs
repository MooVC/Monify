namespace Monify.Strategies.JsonConverterStrategyTests;

using Monify.Model;

public sealed class WhenGenerateIsCalled
{
    [Theory]
    [InlineData("sealed partial class")]
    [InlineData("sealed partial record")]
    [InlineData("partial struct")]
    [InlineData("readonly partial struct")]
    [InlineData("readonly partial record struct")]
    public void GivenSubjectWithoutJsonConverterThenNestedConverterIsGenerated(string declaration)
    {
        // Arrange
        var subject = TestSubject.Create();
        subject.Declaration = declaration;
        subject.Serialization = subject.Serialization.SupportsJsonSerialization(true);
        var strategy = new JsonConverterStrategy();

        // Act
        var source = strategy.Generate(subject).Single();

        // Assert
        source.Hint.ShouldBe("JsonConverter");
        source.Code.ShouldContain("[global::System.Text.Json.Serialization.JsonConverter(typeof(Sample.Converter))]");
        source.Code.ShouldContain("public sealed class Converter : global::System.Text.Json.Serialization.JsonConverter<Sample>");
        source.Code.ShouldContain("return new Sample(global::System.Text.Json.JsonSerializer.Deserialize<int>(ref reader, options));");
        source.Code.ShouldContain("global::System.Text.Json.JsonSerializer.Serialize<int>(writer, value._value, options);");
    }

    [Fact]
    public void GivenSubjectWithJsonConverterThenNoSourceIsGenerated()
    {
        // Arrange
        var subject = TestSubject.Create();
        subject.Serialization = subject.Serialization
            .HasJsonConverter(true)
            .SupportsJsonSerialization(true);
        var strategy = new JsonConverterStrategy();

        // Act
        var sources = strategy.Generate(subject);

        // Assert
        sources.ShouldBeEmpty();
    }

    [Fact]
    public void GivenCompilationWithoutJsonSerializationThenNoSourceIsGenerated()
    {
        // Arrange
        var subject = TestSubject.Create();
        var strategy = new JsonConverterStrategy();

        // Act
        var sources = strategy.Generate(subject);

        // Assert
        sources.ShouldBeEmpty();
    }

    [Theory]
    [InlineData("", "global::Sample_1JsonConverterFactory")]
    [InlineData("Example", "global::Example.Sample_1JsonConverterFactory")]
    public void GivenGenericSubjectThenFactoryAndNestedConverterAreGenerated(string namespaceName, string converterType)
    {
        // Arrange
        var subject = TestSubject.Create();
        subject.Serialization = subject.Serialization
            .WithJsonConverterFactoryName("Sample_1JsonConverterFactory")
            .WithJsonConverterFactoryTarget("Sample`1")
            .SupportsJsonSerialization(true);
        subject.Namespace = namespaceName;
        subject.Qualification = "Sample<T>";
        var strategy = new JsonConverterStrategy();

        // Act
        var sources = strategy.Generate(subject).ToArray();

        // Assert
        sources.Length.ShouldBe(2);
        sources[0].Hint.ShouldBe("JsonConverterFactory");
        sources[0].IsNested.ShouldBeFalse();
        sources[0].Code.ShouldContain("typeToConvert.GetGenericTypeDefinition().FullName == \"Sample`1\"");
        sources[1].Code.ShouldContain($"typeof({converterType})");
        sources[1].Code.ShouldContain("JsonConverter<Sample<T>>");
    }
}