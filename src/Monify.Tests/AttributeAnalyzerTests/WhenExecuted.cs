namespace Monify.AttributeAnalyzerTests;

using System.Text;
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
    public static IEnumerable<TestDataRow<(ReferenceAssemblies Assembly, Expectations Expectations, LanguageVersion Language)>> CompliantCases()
    {
        return SnippetCases.Create(
            exclusions:
            [
                typeof(Attributes),
                typeof(SelfReferencedClass),
                typeof(SelfReferencedRecord),
                typeof(SelfReferencedStruct),
                typeof(UnannotatedClass),
                typeof(UnannotatedRecord),
                typeof(UnannotatedStruct),
                typeof(UnsupportedClass),
                typeof(UnsupportedRecord),
                typeof(UnsupportedStruct),
            ],
            extensions: Extensions.None);
    }

    public static IEnumerable<TestDataRow<(ReferenceAssemblies Assembly, Expectations Expectations, LanguageVersion Language)>> NonPartialCases()
    {
        return SnippetCases.Create(inclusions: [typeof(UnsupportedClass), typeof(UnsupportedRecord), typeof(UnsupportedStruct)]);
    }

    public static IEnumerable<TestDataRow<(ReferenceAssemblies Assembly, Expectations Expectations, LanguageVersion Language)>> SelfReferencingCases()
    {
        return SnippetCases.Create(inclusions: [typeof(SelfReferencedClass), typeof(SelfReferencedRecord), typeof(SelfReferencedStruct)]);
    }

    [Test]
    [MethodDataSource(nameof(CompliantCases))]
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

    [Test]
    [MethodDataSource(typeof(Frameworks), nameof(Frameworks.CSharp11Cases))]
    public async Task GivenAClassWhenNoStateExistsThenCapturesStateRuleIsNotRaised(ReferenceAssemblies assembly, LanguageVersion language)
    {
        // Arrange
        var test = new AnalyzerTest(assembly, language);

        test.TestState.Sources.Add(
            SourceText.From(
                """
                namespace Monify.Tests;

                [Monify<string>]
                public partial class Inner
                {
                }
                """,
                Encoding.UTF8));

        // Act
        Func<Task> act = () => test.RunAsync();

        // Assert
        await act.ShouldNotThrowAsync();
    }

    [Test]
    [MethodDataSource(typeof(Frameworks), nameof(Frameworks.CSharp11Cases))]
    public async Task GivenAPositionalRecordWhenOnlyImplicitStateExistsThenCapturesStateRuleIsRaised(ReferenceAssemblies assembly, LanguageVersion language)
    {
        // Arrange
        var test = new AnalyzerTest(assembly, language);

        test.TestState.Sources.Add(
            SourceText.From(
                """
                namespace Monify.Tests;

                [Monify<int>]
                public partial record Age(int Amount);
                """,
                Encoding.UTF8));

        // Act
        Func<Task> act = () => test.RunAsync();

        // Assert
        _ = await act.ShouldThrowAsync<InvalidOperationException>();
    }

    [Test]
    [MethodDataSource(nameof(NonPartialCases))]
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

    [Test]
    [MethodDataSource(nameof(SelfReferencingCases))]
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