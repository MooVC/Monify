namespace Monify.AttributeAnalyzerTests;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Text;
using Monify;
using Monify.Snippets;
using Monify.Snippets.Declarations;
using AnalyzerTest = Monify.AnalyzerTest<Monify.AttributeAnalyzer>;
using SelfReferencedClass = Monify.Snippets.Declarations.Classes.SelfReferenced;
using SelfReferencedRecord = Monify.Snippets.Declarations.Records.SelfReferenced;
using SelfReferencedStruct = Monify.Snippets.Declarations.Structs.SelfReferenced;
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
            typeof(SelfReferencedClass),
            typeof(SelfReferencedRecord),
            typeof(SelfReferencedStruct),
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

        test.ExpectedDiagnostics.Add(GetExpectedPartialTypeRule(new LinePosition(2, 5), UnsupportedStruct.Declaration.Name));
        expectations.IsDeclaredIn(test.TestState);

        // Act
        Func<Task> act = () => test.RunAsync();

        // Assert
        await act.ShouldNotThrowAsync();
    }

    [Theory]
    [Snippets(exclusions: [typeof(Attributes)], inclusions: [typeof(SelfReferencedClass), typeof(SelfReferencedRecord), typeof(SelfReferencedStruct)])]
    public async Task GivenATypeWhenSelfReferencedThenSelfReferencingRuleIsRaised(ReferenceAssemblies assembly, Expectations expectations, LanguageVersion language)
    {
        // Arrange
        var test = new AnalyzerTest(assembly, language);

        test.ExpectedDiagnostics.Add(GetExpectedSelfReferenceRule(new LinePosition(2, 5), SelfReferencedClass.Declaration.Name));
        expectations.IsDeclaredIn(test.TestState);

        // Act
        Func<Task> act = () => test.RunAsync();

        // Assert
        await act.ShouldNotThrowAsync();
    }

    private static DiagnosticResult GetExpectedPartialTypeRule(LinePosition position, string @class)
    {
        return new DiagnosticResult(AttributeAnalyzer.PartialTypeRule)
            .WithLocation(position)
            .WithArguments(@class);
    }

    private static DiagnosticResult GetExpectedSelfReferenceRule(LinePosition position, string @class)
    {
        return new DiagnosticResult(AttributeAnalyzer.SelfReferenceRule)
            .WithLocation(position)
            .WithArguments(@class);
    }
}