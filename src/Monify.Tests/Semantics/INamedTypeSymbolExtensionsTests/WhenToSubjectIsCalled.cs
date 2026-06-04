namespace Monify.Semantics.INamedTypeSymbolExtensionsTests;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Monify.Semantics;

public sealed class WhenToSubjectIsCalled
{
    [Fact]
    public void GivenStatefulTypeThenSubjectIsStillCreated()
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

            [Monify<int>]
            public sealed partial class Outer
            {
                private readonly string _name;
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
        var subject = outer.ToSubject(compilation, model, [], value);

        // Assert
        _ = subject.ShouldNotBeNull();
        subject!.HasField.ShouldBeFalse();
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
        var subject = outer.ToSubject(compilation, model, [], value);

        // Assert
        _ = subject.ShouldNotBeNull();
        subject!.Encapsulated.Length.ShouldBe(1);
        subject.Encapsulated[0].Type.ShouldBe("global::Sample.Inner");
        subject.Encapsulated[0].HasConversionFrom.ShouldBeFalse();
        subject.Encapsulated[0].HasConversionTo.ShouldBeFalse();
        subject.Encapsulated[0].HasEqualityOperator.ShouldBeFalse();
        subject.Encapsulated[0].HasInequalityOperator.ShouldBeFalse();
    }

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
        var subject = outer.ToSubject(compilation, model, [], value);

        // Assert
        _ = subject.ShouldNotBeNull();
        subject!.Encapsulated.Length.ShouldBe(2);
        subject.Encapsulated[0].Type.ShouldBe("global::Sample.Inner");
        subject.Encapsulated[0].HasConversionFrom.ShouldBeFalse();
        subject.Encapsulated[0].HasConversionTo.ShouldBeFalse();
        subject.Encapsulated[0].HasEqualityOperator.ShouldBeFalse();
        subject.Encapsulated[0].HasInequalityOperator.ShouldBeFalse();
        subject.Encapsulated[1].Type.ShouldBe("string");
        subject.Encapsulated[1].HasConversionFrom.ShouldBeFalse();
        subject.Encapsulated[1].HasConversionTo.ShouldBeFalse();
        subject.Encapsulated[1].HasEqualityOperator.ShouldBeFalse();
        subject.Encapsulated[1].HasInequalityOperator.ShouldBeFalse();
    }

    [Fact]
    public void GivenDebuggerDisplayAttributeThenSubjectDisablesGeneration()
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
            using System.Diagnostics;
            using Monify;

            namespace Sample;

            [DebuggerDisplay("Sample")]
            [Monify<int>]
            public sealed partial class Outer
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
        var subject = outer.ToSubject(compilation, model, [], value);

        // Assert
        _ = subject.ShouldNotBeNull();
        subject!.GenerateDebuggerDisplay.ShouldBeFalse();
    }

    [Fact]
    public void GivenDebuggerDisplayIsFalseThenSubjectCapturesSetting()
    {
        // Arrange
        const string attribute = """
            namespace Monify
            {
                using System;

                [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
                internal sealed class MonifyAttribute<T> : Attribute
                {
                    public bool DebuggerDisplay { get; set; } = true;
                }
            }
            """;

        const string declarations = """
            using Monify;

            namespace Sample;

            [Monify<int>(DebuggerDisplay = false)]
            public sealed partial class Outer
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
        outer.HasMonify(model, out ITypeSymbol value, out bool debuggerDisplay, out _).ShouldBeTrue();

        // Act
        var subject = outer.ToSubject(compilation, model, [], value, debuggerDisplay: debuggerDisplay);

        // Assert
        _ = subject.ShouldNotBeNull();
        subject!.GenerateDebuggerDisplay.ShouldBeFalse();
    }
}