namespace Monify.AttributeGeneratorTests;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Monify.Snippets.Declarations;

public sealed class WhenExecuted
{
    [Test]
    [MethodDataSource(typeof(Frameworks), nameof(Frameworks.DefaultCases))]
    public async Task GivenAnAssemblyThenTheAttributeIsGenerated(ReferenceAssemblies assemblies, LanguageVersion language)
    {
        // Arrange
        var test = new GeneratorTest<AttributeGenerator>(assemblies, language);

        Attributes.IsExpectedIn(test.TestState, language);

        // Act
        Func<Task> act = () => test.RunAsync();

        // Assert
        await act.ShouldNotThrowAsync();
    }
}