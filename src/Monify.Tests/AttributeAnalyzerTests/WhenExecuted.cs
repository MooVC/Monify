namespace Monify.MonifyAttributeAnalyzerTests;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using Monify;
using Monify.Snippets;
using Monify.Snippets.Declarations;
using AnalyzerTest = Monify.AnalyzerTest<Monify.AttributeAnalyzer>;
using UnannotatedClass = Monify.Snippets.Declarations.Classes.Unannotated;
using UnannotatedRecord = Monify.Snippets.Declarations.Records.Unannotated;
using UnannotatedStruct = Monify.Snippets.Declarations.Structs.Unannotated;
using UnsupportedClass = Monify.Snippets.Declarations.Classes.Unsupported;
using UnsupportedRecord = Monify.Snippets.Declarations.Records.Unsupported;
using UnsupportedStruct = Monify.Snippets.Declarations.Structs.Unsupported;

public sealed class WhenExecuted
{
    [Theory]
    [Snippets(
        exclusions:
        [
            typeof(Attributes),
            typeof(UnannotatedClass),
            typeof(UnsupportedClass),
            typeof(UnannotatedRecord),
            typeof(UnsupportedRecord),
            typeof(UnannotatedStruct),
            typeof(UnsupportedStruct),
        ],
        extensions: Extensions.None)]
    public async Task GivenATypeWhenCompliantThenNoDiagnosticsAreRaised(ReferenceAssemblies assembly, Expectations expectations, LanguageVersion language)
    {
        // Arrange
        var test = new AnalyzerTest(assembly, language);

        expectations.IsDeclaredIn(test.TestState);

        // Act
        Func<Task> act = () => test.RunAsync();

        // Assert
        await act.ShouldNotThrowAsync();
    }

    [Theory]
    [Snippets(exclusions: [typeof(Attributes)], inclusions: [typeof(UnsupportedClass), typeof(UnsupportedRecord), typeof(UnsupportedStruct)])]
    public async Task GivenATypeWhenNotPartialThenPartialTypeRuleIsRaised(ReferenceAssemblies assembly, Expectations expectations, LanguageVersion language)
    {
        // Arrange
        var test = new AnalyzerTest(assembly, language);

        test.ExpectedDiagnostics.Add(GetExpectedPartialThePartialTypeRule(new LinePosition(2, 5), UnsupportedStruct.Declaration.Name));
        expectations.IsDeclaredIn(test.TestState);

        // Act
        Func<Task> act = () => test.RunAsync();

        // Assert
        await act.ShouldNotThrowAsync();
    }

    [Theory]
    [Frameworks(Language = LanguageVersion.CSharp2)]
    public async Task GivenATypeWhenReferencingItselfThenSelfReferenceRuleIsRaised(ReferenceAssemblies assembly, LanguageVersion language)
    {
        // Arrange
        foreach (string annotation in ["[Monify<Age>]", "[Monify(Type = typeof(Age))]"])
        {
            var test = new AnalyzerTest(assembly, language);

            test.TestState.Sources.Add($$"""
            using Monify;

            namespace Monify.Testing;

            {{annotation}}
            public partial record Age;
            """);

            test.ExpectedDiagnostics.Add(new DiagnosticResult(AttributeAnalyzer.SelfReferenceRule).WithArguments("Age"));

            // Act
            Func<Task> act = () => test.RunAsync();

            // Assert
            await act.ShouldNotThrowAsync();
        }
    }

    private static DiagnosticResult GetExpectedPartialThePartialTypeRule(LinePosition position, string @class)
    {
        return new DiagnosticResult(AttributeAnalyzer.PartialTypeRule)
            .WithLocation(position)
            .WithArguments(@class);
    }
}