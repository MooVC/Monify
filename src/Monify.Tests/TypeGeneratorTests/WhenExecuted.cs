namespace Monify.TypeGeneratorTests;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Monify.Snippets;
using Monify.Snippets.Declarations;

public sealed partial class WhenExecuted
{
    private static readonly Type[] _generators =
    [
        typeof(AttributeGenerator),
        typeof(HashCodeGenerator),
        typeof(SequenceEqualityComparerGenerator),
        typeof(TypeGenerator),
    ];

    [Theory]
    [Snippets(exclusions: [typeof(Attributes)])]
    public async Task GivenATypeTheExpectedSourceIsGenerated(ReferenceAssemblies assembly, Expectations expectations, LanguageVersion language)
    {
        // Arrange
        var test = new GeneratorTest<TypeGenerator>(assembly, language, _generators);

        Attributes.IsExpectedIn(test.TestState, language);
        Internal.HashCode.IsExpectedIn(test.TestState);
        Internal.SequenceEqualityComparer.IsExpectedIn(test.TestState);
        var references = await assembly.ResolveAsync(LanguageNames.CSharp, CancellationToken.None);
        bool supportsJsonSerialization = references.OfType<PortableExecutableReference>()
            .Any(reference => Path.GetFileName(reference.FilePath) == "System.Text.Json.dll");
        expectations.IsDeclaredIn(test.TestState, supportsJsonSerialization);

        // Act
        Func<Task> act = () => test.RunAsync();

        // Assert
        await act.ShouldNotThrowAsync();
    }
}