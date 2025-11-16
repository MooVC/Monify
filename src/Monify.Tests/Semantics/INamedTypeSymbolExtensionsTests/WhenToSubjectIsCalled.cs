namespace Monify.Semantics.INamedTypeSymbolExtensionsTests;

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Monify.Model;
using Monify.Semantics;

public sealed class WhenToSubjectIsCalled
{
    [Fact]
    public void GivenSubjectEncapsulatingMonifiedTypeThenConversionsAreIncluded()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute<T> : Attribute
                {
                }
            }
            """;

        const string declarations = """
            using Monify;

            namespace Sample;

            [Monify<Inner>]
            public sealed partial class Outer
            {
            }

            [Monify<string>]
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
        var subject = outer.ToSubject(compilation, [], value);

        // Assert
        _ = subject.ShouldNotBeNull();
        subject!.Operators.Length.ShouldBe(2);
        subject.Operators[0].Type.ShouldBe("global::Sample.Inner");
        subject.Operators[0].HasConversionFrom.ShouldBeFalse();
        subject.Operators[0].HasConversionTo.ShouldBeFalse();
        subject.Operators[0].HasEqualityOperator.ShouldBeFalse();
        subject.Operators[0].HasInequalityOperator.ShouldBeFalse();
        subject.Operators[1].Type.ShouldBe("string");
        subject.Operators[1].HasConversionFrom.ShouldBeFalse();
        subject.Operators[1].HasConversionTo.ShouldBeFalse();
        subject.Operators[1].HasEqualityOperator.ShouldBeFalse();
        subject.Operators[1].HasInequalityOperator.ShouldBeFalse();
    }

    [Fact]
    public void GivenCircularEncapsulationThenConversionDetectionIsTerminated()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute<T> : Attribute
                {
                }
            }
            """;

        const string declarations = """
            using Monify;

            namespace Sample;

            [Monify<Inner>]
            public sealed partial class Outer
            {
            }

            [Monify<Outer>]
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
        var subject = outer.ToSubject(compilation, [], value);

        // Assert
        _ = subject.ShouldNotBeNull();
        subject!.Operators.Length.ShouldBe(1);
        subject.Operators[0].Type.ShouldBe("global::Sample.Inner");
        subject.Operators[0].HasConversionFrom.ShouldBeFalse();
        subject.Operators[0].HasConversionTo.ShouldBeFalse();
        subject.Operators[0].HasEqualityOperator.ShouldBeFalse();
        subject.Operators[0].HasInequalityOperator.ShouldBeFalse();
    }
}