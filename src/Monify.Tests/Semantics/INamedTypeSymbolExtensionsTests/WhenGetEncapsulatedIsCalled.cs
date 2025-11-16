namespace Monify.Semantics.INamedTypeSymbolExtensionsTests;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Monify.Model;
using Monify.Semantics;

public sealed class WhenGetEncapsulatedIsCalled
{
    [Fact]
    public void GivenMonifyAttributesWithNamedArgumentsThenPassthroughValuesAreCaptured()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute : Attribute
                {
                    public Type? Type { get; set; }
                }
            }
            """;

        const string declarations = """
            using Monify;

            namespace Sample;

            [Monify(Type = typeof(Inner))]
            public sealed partial class Outer
            {
            }

            [Monify(Type = typeof(string))]
            public sealed partial class Inner
            {
            }
            """;

        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree[] trees =
        [
            CSharpSyntaxTree.ParseText(attribute, options),
            CSharpSyntaxTree.ParseText(declarations, options),
        ];

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        var compilation = CSharpCompilation.Create(
            "Sample",
            trees,
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        SemanticModel model = compilation.GetSemanticModel(trees[1]);
        INamedTypeSymbol? outer = compilation.GetTypeByMetadataName("Sample.Outer");

        _ = outer.ShouldNotBeNull();
        outer.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        // Act
        ImmutableArray<Encapsulated> encapsulated = outer.GetEncapsulated(compilation, value);

        // Assert
        encapsulated.Length.ShouldBe(2);
        encapsulated[0].Type.ShouldBe("global::Sample.Inner");
        encapsulated[1].Type.ShouldBe("string");
    }
}
