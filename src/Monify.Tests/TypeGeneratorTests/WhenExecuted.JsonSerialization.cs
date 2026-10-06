namespace Monify.TypeGeneratorTests;

using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Monify.Semantics;

public sealed partial class WhenExecuted
{
    private const string Json = "42";
    private const string JsonConverterHint = ".Sample.JsonConverter.g.cs";
    private const int SampleValue = 42;

    [Theory]
    [InlineData("partial class")]
    [InlineData("partial record")]
    [InlineData("partial struct")]
    [InlineData("readonly partial struct")]
    [InlineData("partial record struct")]
    public void GivenUnannotatedTypeThenGeneratedConverterRoundTripsValue(string declaration)
    {
        // Arrange
        string code = $$"""
            using Monify;
            [Monify<int>(Passthrough = false)]
            public {{declaration}} Sample { }
            """;

        // Act
        var (assembly, result) = Generate(code);
        var type = assembly.GetType("Sample").ShouldNotBeNull();
        var instance = Activator.CreateInstance(type, SampleValue).ShouldNotBeNull();
        string json = JsonSerializer.Serialize(instance, type);
        var restored = JsonSerializer.Deserialize(json, type).ShouldNotBeNull();

        // Assert
        result.Results.SelectMany(generator => generator.GeneratedSources)
            .ShouldContain(source => source.HintName == JsonConverterHint);
        type.GetCustomAttribute<JsonConverterAttribute>().ShouldNotBeNull()
            .ConverterType.ShouldBe(type.GetNestedType("Converter"));
        json.ShouldBe(Json);
        restored.ShouldBe(instance);
    }

    [Theory]
    [InlineData("Sample", "public partial class Sample { }")]
    [InlineData("Sample`1", "public partial class Sample<T> { }")]
    [InlineData("Outer`1+Sample", "public partial class Outer<T> { public partial class Sample { } }")]
    public void GivenConflictingFrameworkTypeNamesThenGeneratedConverterRoundTripsValue(string typeName, string declaration)
    {
        // Arrange
        const string namespaceName = "MooVC.Syntax.CSharp";
        declaration = declaration.Replace(
            "public partial class Sample",
            "[Monify<int>(Passthrough = false)] public partial class Sample",
            StringComparison.Ordinal);
        string code = $$"""
            using Monify;

            namespace {{namespaceName}}
            {
                public abstract class Type { }
                public static class Activator { }
                {{declaration}}
            }
            """;

        // Act
        var (assembly, _) = Generate(code);
        var type = assembly.GetType(namespaceName + "." + typeName).ShouldNotBeNull();

        if (type.IsGenericTypeDefinition)
        {
            var arguments = Enumerable.Repeat(typeof(int), type.GetGenericArguments().Length).ToArray();
            type = type.MakeGenericType(arguments);
        }

        var instance = Activator.CreateInstance(type, SampleValue).ShouldNotBeNull();
        string json = JsonSerializer.Serialize(instance, type);
        var restored = JsonSerializer.Deserialize(json, type).ShouldNotBeNull();

        // Assert
        json.ShouldBe(Json);
        restored.ShouldBe(instance);
    }

    [Theory]
    [InlineData("Sample`1", "public partial class Sample<T> { }", "Sample_1JsonConverterFactory")]
    [InlineData("Outer`1+Sample", "public partial class Outer<T> { public partial class Sample { } }", "Outer_1_SampleJsonConverterFactory")]
    [InlineData("Outer`1+Sample`1", "public partial class Outer<T> { public partial class Sample<TValue> { } }", "Outer_1_Sample_1JsonConverterFactory")]
    [InlineData("Outer`1+Sample", "public partial class Outer<T> { private partial class Sample { } }", "Outer_1_SampleJsonConverterFactory")]
    public void GivenGenericTypeThenFactoryRoundTripsValue(string typeName, string declaration, string factoryName)
    {
        // Arrange
        string code = "using Monify; " + declaration;
        code = code.Replace("public partial class Sample", "[Monify<int>(Passthrough = false)] public partial class Sample", StringComparison.Ordinal)
            .Replace("private partial class Sample", "[Monify<int>(Passthrough = false)] private partial class Sample", StringComparison.Ordinal);

        // Act
        var (assembly, _) = Generate(code);
        var definition = assembly.GetType(typeName).ShouldNotBeNull();
        var arguments = Enumerable.Repeat(typeof(int), definition.GetGenericArguments().Length).ToArray();
        var type = definition.MakeGenericType(arguments);
        var instance = Activator.CreateInstance(type, SampleValue).ShouldNotBeNull();
        string json = JsonSerializer.Serialize(instance, type);
        var restored = JsonSerializer.Deserialize(json, type).ShouldNotBeNull();

        // Assert
        type.GetCustomAttribute<JsonConverterAttribute>().ShouldNotBeNull()
            .ConverterType.ShouldBe(assembly.GetType(factoryName));
        json.ShouldBe(Json);
        restored.ShouldBe(instance);
    }

    [Fact]
    public void GivenAnnotatedTypeThenExistingConverterIsPreserved()
    {
        // Arrange
        const string customJson = "\"custom\"";
        const string code = """
            using System;
            using System.Text.Json;
            using System.Text.Json.Serialization;
            using Monify;

            [Monify<int>(Passthrough = false)]
            [JsonConverter(typeof(ExistingConverter))]
            public partial class Sample { }

            public sealed class ExistingConverter : JsonConverter<Sample>
            {
                public override Sample Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => new Sample(42);
                public override void Write(Utf8JsonWriter writer, Sample value, JsonSerializerOptions options) => writer.WriteStringValue("custom");
            }
            """;
        var compilation = JsonCompilation.Create(code);
        var symbol = compilation.GetTypeByMetadataName("Sample").ShouldNotBeNull();
        var subject = symbol.ToSubject(compilation, compilation.GetSemanticModel(compilation.SyntaxTrees.Single()), [], compilation.GetSpecialType(SpecialType.System_Int32));

        // Act
        var (assembly, result) = Generate(code);
        var type = assembly.GetType("Sample").ShouldNotBeNull();
        var instance = Activator.CreateInstance(type, SampleValue).ShouldNotBeNull();
        string json = JsonSerializer.Serialize(instance, type);
        var restored = JsonSerializer.Deserialize(json, type).ShouldNotBeNull();

        // Assert
        subject.ShouldNotBeNull().Serialization.HasJsonConverter.ShouldBeTrue();
        result.Results.SelectMany(generator => generator.GeneratedSources)
            .ShouldNotContain(source => source.HintName.Contains("JsonConverter", StringComparison.Ordinal));
        type.GetNestedType("Converter").ShouldBeNull();
        json.ShouldBe(customJson);
        restored.ShouldBe(instance);
    }

    [Theory]
    [InlineData("string", "\"sample\"")]
    [InlineData("int[]", "[1,2,3]")]
    [InlineData("int?", "null")]
    [InlineData("string", "null")]
    public void GivenEncapsulatedValueThenJsonShapeIsPreserved(string valueType, string json)
    {
        // Arrange
        string code = $$"""
            using Monify;
            [Monify<{{valueType}}>]
            public partial struct Sample { }
            """;

        // Act
        var (assembly, _) = Generate(code);
        var type = assembly.GetType("Sample").ShouldNotBeNull();
        var restored = JsonSerializer.Deserialize(json, type).ShouldNotBeNull();
        string actual = JsonSerializer.Serialize(restored, type);

        // Assert
        actual.ShouldBe(json);
    }

    [Fact]
    public void GivenSerializerOptionsThenOptionsAreForwardedToEncapsulatedValue()
    {
        // Arrange
        const string expectedJson = "\"42\"";
        const string code = """
            using Monify;
            [Monify<int>(Passthrough = false)]
            public partial struct Sample { }
            """;
        var options = new JsonSerializerOptions
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString,
        };

        // Act
        var (assembly, _) = Generate(code);
        var type = assembly.GetType("Sample").ShouldNotBeNull();
        var restored = JsonSerializer.Deserialize(expectedJson, type, options).ShouldNotBeNull();
        string actual = JsonSerializer.Serialize(restored, type, options);

        // Assert
        actual.ShouldBe(expectedJson);
        restored.ToString().ShouldBe(Json);
    }

    private static (Assembly Assembly, GeneratorDriverRunResult Result) Generate(string code)
    {
        var compilation = JsonCompilation.Create(code);
        ISourceGenerator[] generators =
        [
            new AttributeGenerator().AsSourceGenerator(),
            new HashCodeGenerator().AsSourceGenerator(),
            new SequenceEqualityComparerGenerator().AsSourceGenerator(),
            new TypeGenerator().AsSourceGenerator(),
        ];
        var options = (CSharpParseOptions)compilation.SyntaxTrees.Single().Options;
        GeneratorDriver driver = CSharpGeneratorDriver.Create(generators, parseOptions: options);
        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out var output, out var diagnostics);
        diagnostics.ShouldBeEmpty();

        using var stream = new MemoryStream();
        var emitted = output.Emit(stream);
        emitted.Diagnostics.Where(diagnostic => diagnostic.Severity == DiagnosticSeverity.Error).ShouldBeEmpty();
        emitted.Success.ShouldBeTrue();

        return (Assembly.Load(stream.ToArray()), driver.GetRunResult());
    }
}