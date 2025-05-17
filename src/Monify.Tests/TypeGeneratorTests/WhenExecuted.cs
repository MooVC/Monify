namespace Monify.TypeGeneratorTests;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Monify.Snippets;
using Monify.Snippets.Declarations;
using Monify.Snippets.Declarations.Records;

public sealed class WhenExecuted
{
    private static readonly Type[] generators =
    [
        typeof(AttributeGenerator),
        typeof(TypeGenerator),
    ];

    [Theory]
    [Snippets(exclusions: [typeof(Simple)])]
    public async Task GivenATypeTheExpectedSourceIsGenerated(ReferenceAssemblies assembly, Expectations expectations, LanguageVersion language)
    {
        // Arrange
        var test = new GeneratorTest<TypeGenerator>(assembly, language, generators);

        expectations.IsDeclaredIn(test.TestState);

        Attributes.Monify.IsExpectedIn(test.TestState);

        // Act
        Func<Task> act = () => test.RunAsync();

        // Assert
        await act.ShouldNotThrowAsync();
    }
}