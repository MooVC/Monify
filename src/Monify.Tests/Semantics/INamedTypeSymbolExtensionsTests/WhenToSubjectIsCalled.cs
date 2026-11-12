namespace Monify.Semantics.INamedTypeSymbolExtensionsTests;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Monify.Semantics;

public sealed class WhenToSubjectIsCalled
{
    [Test]
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

    [Test]
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
        subject!.EncapsulatedValues.Length.ShouldBe(1);
        subject.EncapsulatedValues[0].Type.ShouldBe("global::Sample.Inner");
        subject.EncapsulatedValues[0].HasConversionFrom.ShouldBeFalse();
        subject.EncapsulatedValues[0].HasConversionTo.ShouldBeFalse();
        subject.EncapsulatedValues[0].HasEqualityOperator.ShouldBeFalse();
        subject.EncapsulatedValues[0].HasInequalityOperator.ShouldBeFalse();
    }

    [Test]
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
        subject!.EncapsulatedValues.Length.ShouldBe(2);
        subject.EncapsulatedValues[0].Type.ShouldBe("global::Sample.Inner");
        subject.EncapsulatedValues[0].HasConversionFrom.ShouldBeFalse();
        subject.EncapsulatedValues[0].HasConversionTo.ShouldBeFalse();
        subject.EncapsulatedValues[0].HasEqualityOperator.ShouldBeFalse();
        subject.EncapsulatedValues[0].HasInequalityOperator.ShouldBeFalse();
        subject.EncapsulatedValues[1].Type.ShouldBe("string");
        subject.EncapsulatedValues[1].HasConversionFrom.ShouldBeFalse();
        subject.EncapsulatedValues[1].HasConversionTo.ShouldBeFalse();
        subject.EncapsulatedValues[1].HasEqualityOperator.ShouldBeFalse();
        subject.EncapsulatedValues[1].HasInequalityOperator.ShouldBeFalse();
    }

    [Test]
    public void GivenDuplicatePassthroughOperatorsThenOnlyTheNearestSignatureIsKept()
    {
        // Arrange
        const string declarations = """
            namespace Monify
            {
                internal sealed class MonifyAttribute<T> : System.Attribute
                {
                }
            }

            namespace Sample
            {
                [Monify.Monify<Inner>]
                public sealed partial class Outer
                {
                }

                [Monify.Monify<Value>]
                public sealed partial class Inner
                {
                    public static Inner operator +(Inner left, Inner right) => left;
                    public static Inner operator -(Inner value) => value;
                }

                public sealed class Value
                {
                    public static Value operator +(Value left, Value right) => left;
                    public static Value operator +(Value left, int right) => left;
                    public static Value operator -(Value value) => value;
                    public static Value operator +(Value value) => value;
                }
            }
            """;

        SyntaxTree tree = CSharpSyntaxTree.ParseText(declarations, new CSharpParseOptions(LanguageVersion.CSharp11));
        var compilation = CSharpCompilation.Create(
            "Sample",
            [tree],
            [MetadataReference.CreateFromFile(typeof(object).Assembly.Location)],
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        SemanticModel model = compilation.GetSemanticModel(tree);
        INamedTypeSymbol outer = compilation.GetTypeByMetadataName("Sample.Outer").ShouldNotBeNull();
        outer.HasMonify(model, out ITypeSymbol value).ShouldBeTrue();

        // Act
        var subject = outer.ToSubject(compilation, model, [], value).ShouldNotBeNull();

        // Assert
        subject.EncapsulatedValues.Length.ShouldBe(2);
        var nearest = subject.EncapsulatedValues[0];
        nearest.BinaryOperators.Single().IsRightSubject.ShouldBeTrue();
        nearest.UnaryOperators.Single().Operator.ShouldBe("op_UnaryNegation");

        var passthrough = subject.EncapsulatedValues[1];
        passthrough.BinaryOperators.Single().Right.ShouldBe("int");
        passthrough.UnaryOperators.Single().Operator.ShouldBe("op_UnaryPlus");
    }

    [Test]
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

    [Test]
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