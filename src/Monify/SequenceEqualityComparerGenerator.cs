namespace Monify
{
    using Microsoft.CodeAnalysis;

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
        public static readonly string Content = string.Format(GeneratedSource).NormalizeLineEndings();

        /// <inheritdoc/>
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(productionContext => productionContext.AddSource("Monify.Internal.SequenceEqualityComparer.g.cs", Content));
        }
    }
}