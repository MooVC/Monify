namespace Monify.Semantics.INamedTypeSymbolExtensionsTests;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Monify.Semantics;

public sealed class WhenHasDebuggerDisplayIsCalled
{
    private const string Declarations = """
        using System.Diagnostics;

        namespace Sample
        {
            [DebuggerDisplay("Sample")]
            public sealed class Decorated
            {
            }

            public sealed class Plain
            {
            }
        }

        namespace Other
        {
            using System;

            [AttributeUsage(AttributeTargets.Class)]
            public sealed class DebuggerDisplayAttribute : Attribute
            {
                public DebuggerDisplayAttribute(string value)
                {
                }
            }

            [DebuggerDisplay("Sample")]
            public sealed class Decorated
            {
            }
        }
        """;

    [Fact]
    public void GivenDebuggerDisplayAttributeThenTrueIsReturned()
    {
        // Arrange
        Compilation compilation = CreateCompilation();
        INamedTypeSymbol? subject = compilation.GetTypeByMetadataName("Sample.Decorated");

        _ = subject.ShouldNotBeNull();

        // Act
        bool result = subject.HasDebuggerDisplay();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void GivenLookalikeDebuggerDisplayAttributeThenFalseIsReturned()
    {
        // Arrange
        Compilation compilation = CreateCompilation();
        INamedTypeSymbol? subject = compilation.GetTypeByMetadataName("Other.Decorated");

        _ = subject.ShouldNotBeNull();

        // Act
        bool result = subject.HasDebuggerDisplay();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void GivenNoDebuggerDisplayAttributeThenFalseIsReturned()
    {
        // Arrange
        Compilation compilation = CreateCompilation();
        INamedTypeSymbol? subject = compilation.GetTypeByMetadataName("Sample.Plain");

        _ = subject.ShouldNotBeNull();

        // Act
        bool result = subject.HasDebuggerDisplay();

        // Assert
        result.ShouldBeFalse();
    }

    private static Compilation CreateCompilation()
    {
        CSharpParseOptions options = new(LanguageVersion.CSharp11);
        SyntaxTree tree = CSharpSyntaxTree.ParseText(Declarations, options);

        MetadataReference[] references =
        [
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
        ];

        return CSharpCompilation.Create(
            "Sample",
            [tree],
            references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }
}