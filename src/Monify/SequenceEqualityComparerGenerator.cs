namespace Monify
{
    using Microsoft.CodeAnalysis;
    using Monify.Syntax;

    using static Monify.SequenceEqualityComparerGenerator_Resources;

    /// <summary>
    /// Generates an internal SequenceEqualityComparer static class that is used to support enumerable enumerable checks.
    /// </summary>
    [Generator(LanguageNames.CSharp)]
    public sealed class SequenceEqualityComparerGenerator
        : IIncrementalGenerator
    {
        /// <summary>
        /// The source code that will be output by the generator.
        /// </summary>
        public static readonly string Content = GenerationSyntax.Block(
            GenerationSyntax.Block(GenerationSyntax.ToSnippet(GeneratedSource), "internal sealed class SequenceEqualityComparer"),
            "namespace Monify.Internal").ToString().NormalizeLineEndings();

        /// <inheritdoc/>
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(productionContext => productionContext.AddSource("Monify.Internal.SequenceEqualityComparer.g.cs", Content));
        }
    }
}