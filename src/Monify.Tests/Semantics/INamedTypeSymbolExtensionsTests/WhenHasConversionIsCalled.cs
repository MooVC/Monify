namespace Monify.Semantics.INamedTypeSymbolExtensionsTests;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Monify.Semantics;

public sealed class WhenHasConversionIsCalled
{
    [Fact]
    public void GivenNullableValueTypeThenConversionIsNotDetectedForNonNullableValueType()
    {
        // Arrange
        const string declarations = """
            namespace Sample;

            public sealed class Wrapper
            {
                public static implicit operator Wrapper(int? value) => new();
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree tree = CSharpSyntaxTree.ParseText(declarations, options);

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            [tree],
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        INamedTypeSymbol? wrapper = compilation.GetTypeByMetadataName("Sample.Wrapper");
        ITypeSymbol? @int = compilation.GetSpecialType(SpecialType.System_Int32);

        _ = wrapper.ShouldNotBeNull();
        _ = @int.ShouldNotBeNull();

        // Act
        bool hasConversionTo = wrapper.HasConversion(@int, wrapper);

        // Assert
        hasConversionTo.ShouldBeFalse();
    }

    [Fact]
    public void GivenNonNullableValueTypeThenConversionIsDetected()
    {
        // Arrange
        const string declarations = """
            namespace Sample;

            public sealed class Wrapper
            {
                public static implicit operator int(Wrapper subject) => 0;

                public static implicit operator Wrapper(int value) => new();
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree tree = CSharpSyntaxTree.ParseText(declarations, options);

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            [tree],
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        INamedTypeSymbol? wrapper = compilation.GetTypeByMetadataName("Sample.Wrapper");
        ITypeSymbol? @int = compilation.GetSpecialType(SpecialType.System_Int32);

        _ = wrapper.ShouldNotBeNull();
        _ = @int.ShouldNotBeNull();

        // Act
        bool hasConversionFrom = wrapper.HasConversion(wrapper, @int);
        bool hasConversionTo = wrapper.HasConversion(@int, wrapper);

        // Assert
        hasConversionFrom.ShouldBeTrue();
        hasConversionTo.ShouldBeTrue();
    }

    [Fact]
    public void GivenNullableReferenceAnnotationsThenConversionIsDetected()
    {
        // Arrange
        const string declarations = """
            #nullable enable

            namespace Sample;

            public sealed class Wrapper
            {
                public static implicit operator string?(Wrapper subject) => string.Empty;

                public static implicit operator Wrapper(string? value) => new();
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree tree = CSharpSyntaxTree.ParseText(declarations, options);

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            [tree],
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        INamedTypeSymbol? wrapper = compilation.GetTypeByMetadataName("Sample.Wrapper");
        ITypeSymbol? @string = compilation.GetSpecialType(SpecialType.System_String);

        _ = wrapper.ShouldNotBeNull();
        _ = @string.ShouldNotBeNull();

        // Act
        bool hasConversionFrom = wrapper.HasConversion(wrapper, @string);
        bool hasConversionTo = wrapper.HasConversion(@string, wrapper);

        // Assert
        hasConversionFrom.ShouldBeTrue();
        hasConversionTo.ShouldBeTrue();
    }
}